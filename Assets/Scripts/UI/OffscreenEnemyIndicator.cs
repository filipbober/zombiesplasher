using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OffscreenEnemyIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyIndicator;

    private ObjectPooler _enemyIndicatorPool;

    private List<GameObject> _enemyList;

    private GameObject _tmpInstatiated;

    void Awake()
    {
        _enemyList = new List<GameObject>();

        //_enemyIndicatorPool = gameObject.AddComponent(ObjectPooler);
    }

    void OnEnable()
    {
        GameManager.EnemySpawnedNotification += AddEnemyToList;
    }

    void OnDisable()
    {
        GameManager.EnemySpawnedNotification -= AddEnemyToList;
    }

    void Update()
    {
        foreach (GameObject go in _enemyList)
        {
            if (!go.activeInHierarchy) return;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
            bool isOffscreen = !(screenPos.z > 0
                && screenPos.x > 0 && screenPos.x < Screen.width
                && screenPos.y > 0 && screenPos.y < Screen.height);

            if (isOffscreen)
            {
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

                GameObject enemyIndicator;
                if (_tmpInstatiated == null)
                {
                    enemyIndicator = Instantiate(_enemyIndicator);
                    //GameObject enemyIndicator = _enemyIndicatorPool.GetPooledObject();
                    enemyIndicator.transform.SetParent(gameObject.transform, false);    // false to scale image with Canvas
                    _tmpInstatiated = enemyIndicator;
                }
                else
                {
                    enemyIndicator = _tmpInstatiated;
                }

                Image enemyIndicatorImage = enemyIndicator.GetComponent<Image>();       // TODO: Make list with pooled objects
                Vector3 viewPos = Camera.main.WorldToViewportPoint(go.transform.position);
                Vector2 indicatorPos = new Vector2(Mathf.Clamp(viewPos.x, 0f, 1f), Mathf.Clamp(viewPos.y, 0f, 1f));

                CanvasScaler scaler = GetComponentInParent<CanvasScaler>();

                float scaleX = scaler.referenceResolution.x / Screen.width;
                float scaleY = scaler.referenceResolution.y / Screen.height;

                Vector2 imgSizeModifier = new Vector2(0f, 0f);
                if (screenPos.x < 0)
                {
                    imgSizeModifier.x += enemyIndicatorImage.rectTransform.rect.width / 2;
                }
                else if (screenPos.x > Screen.width)
                {
                    imgSizeModifier.x -= enemyIndicatorImage.rectTransform.rect.width / 2;
                }

                if (screenPos.y < 0)
                {
                    imgSizeModifier.y += enemyIndicatorImage.rectTransform.rect.height / 2;
                }
                else if (screenPos.y > Screen.height)
                {
                    imgSizeModifier.y -= enemyIndicatorImage.rectTransform.rect.height / 2;
                }

                Vector2 borderPosition = new Vector2((indicatorPos.x * Screen.width - Screen.width / 2) * scaleX, (indicatorPos.y * Screen.height - Screen.height / 2) * scaleY);
                borderPosition += imgSizeModifier;
                enemyIndicatorImage.rectTransform.anchoredPosition = borderPosition;

                //return;
            }
        }
    }

    void AddEnemyToList(object sender, EnemyPropertiesEventArgs e)
    {
        if (!_enemyList.Contains(e.EnemyGameObj))
        {
            _enemyList.Add(e.EnemyGameObj);
        }
    }
}

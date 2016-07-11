using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OffscreenEnemyIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyIndicator;

    private List<GameObject> _enemyList;

    private GameObject _tmpInstatiated;

    void Awake()
    {
        _enemyList = new List<GameObject>();
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
            Vector3 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);

            if (!(screenPos.z > 0
                && screenPos.x > 0 && screenPos.x < Screen.width
                && screenPos.y > 0 && screenPos.y < Screen.height))
            {
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

                GameObject enemyIndicator;
                if (_tmpInstatiated == null)
                {
                    enemyIndicator = Instantiate(_enemyIndicator);
                    enemyIndicator.transform.SetParent(gameObject.transform);
                    _tmpInstatiated = enemyIndicator;
                }
                else
                {
                    enemyIndicator = _tmpInstatiated;
                }

                Image enemyIndicatorImage = enemyIndicator.GetComponent<Image>();

                Vector3 viewPos = Camera.main.WorldToViewportPoint(go.transform.position);
                Debug.Log(viewPos);
                Vector2 indicatorPos = new Vector2(Mathf.Clamp(viewPos.x, 0f, 1f), Mathf.Clamp(viewPos.y, 0f, 1f));

                //enemyIndicatorImage.rectTransform.anchoredPosition = new Vector2(viewPos.x, viewPos.y);
                //enemyIndicatorImage.rectTransform.anchoredPosition = indicatorPos;
                //enemyIndicatorImage.rectTransform.position = indicatorPos;
                //enemyIndicatorImage.rectTransform.anchoredPosition = new Vector2(0.9f, 0.9f);
                //enemyIndicator.transform.position = new Vector2(0.9f, 0.9f);


                CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
                //enemyIndicatorImage.rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width, Input.mousePosition.y * scaler.referenceResolution.y / Screen.height);
                //enemyIndicatorImage.rectTransform.anchoredPosition = new Vector2(indicatorPos.x * scaler.referenceResolution.x / Screen.width, indicatorPos.y * scaler.referenceResolution.y / Screen.height);
                //enemyIndicatorImage.rectTransform.anchoredPosition = new Vector2(0.1f * scaler.referenceResolution.x, 0.1f);

                // ----
                //if (screenPos.x < 0)
                //{
                //    indicatorPos.x = -Screen.width / 2;
                //}
                //else if (screenPos.x > Screen.width)
                //{
                //    indicatorPos.x = Screen.width / 2;
                //}

                //if (screenPos.y < 0)
                //{
                //    indicatorPos.y = -Screen.height / 2;
                //}
                //else if (screenPos.y > Screen.height)
                //{
                //    indicatorPos.y = Screen.height / 2;
                //}

                //enemyIndicatorImage.rectTransform.anchoredPosition = new Vector2(indicatorPos.x, indicatorPos.y);
                // ----

                enemyIndicatorImage.rectTransform.anchoredPosition = new Vector2(indicatorPos.x * Screen.width - Screen.width/2, indicatorPos.y * Screen.height - Screen.height/2);


                return;
            }
        }
    }

    void AddEnemyToList(object sender, EnemyPropertiesEventArgs e)
    {
        _enemyList.Add(e.EnemyGameObj);
    }
}

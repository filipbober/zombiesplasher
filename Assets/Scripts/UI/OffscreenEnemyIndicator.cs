using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// TDOD:
// - remove GetComponent calls
public class OffscreenEnemyIndicator : MonoBehaviour
{
    //private struct EnemyIndicator
    //{
    //    public EnemyIndicator(GameObject gameObj, Image imageComponent)
    //    {
    //        GameObj = gameObj;
    //        ImageComponent = imageComponent;          
    //    }

    //    public GameObject GameObj;
    //    public Image ImageComponent;
    //};

    [SerializeField]
    private Canvas _hudCavas;

    //[SerializeField]
    //private GameObject _enemyIndicator;

    [SerializeField]
    private ObjectPooler _enemyIndicatorPool;

    private List<GameObject> _enemies;
    //private List<Image> _enemies;
    //private List<Image> _indicatorImages; 
    //private List<EnemyIndicator> _enemyIndicators;
    private List<GameObject> _activeIndicators;

    private GameObject _tmpInstatiated;

    private CanvasScaler _canvasScaler;
    private Vector2 _referenceResolution;
    private int _screenWidth;
    private int _screenHeight;

    void Awake()
    {
        _canvasScaler = _hudCavas.GetComponent<CanvasScaler>();
        _enemies = new List<GameObject>();
        _activeIndicators = new List<GameObject>();
        //        _enemyIndicatorPool = GetComponent<ObjectPooler>();
        //_enemyIndicators = new List<EnemyIndicator>();

        UpdateScreenData();
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
        UpdateScreenData();


        // --------------------------------------------
        List<GameObject> offscreenEnemies;
        offscreenEnemies = new List<GameObject>(_enemies.Count);

        foreach (GameObject go in _enemies)
        {
            if (!go.gameObject.activeInHierarchy) return;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
            bool isOffscreen = !(screenPos.z > 0
                && screenPos.x > 0 && screenPos.x < Screen.width
                && screenPos.y > 0 && screenPos.y < Screen.height);

            if (!offscreenEnemies.Contains(go))
            {
                offscreenEnemies.Add(go);
            }

        }

        //Vector3 screenCenter2 = new Vector3(_screenWidth / 2, _screenHeight / 2);

        foreach (GameObject indicator in _activeIndicators)
        {
            indicator.gameObject.SetActive(false);
        }
        _activeIndicators = new List<GameObject>(offscreenEnemies.Count);

        foreach (GameObject go in offscreenEnemies)
        {
            GameObject newIndicator = _enemyIndicatorPool.GetPooledObject();
            newIndicator.SetActive(true);
            newIndicator.transform.SetParent(_hudCavas.transform, false);
            _activeIndicators.Add(newIndicator);

            Image indicatorImage = newIndicator.GetComponent<Image>();
            Vector3 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);

            Vector2 imageOffset = ComputeImageOffset(indicatorImage, screenPos);
            Vector2 borderPosition = ComputeBorderPosition(go.transform.position);
            borderPosition += imageOffset;

            indicatorImage.rectTransform.anchoredPosition = borderPosition;
        }

        return;
        // ---------------------------------------------

        foreach (GameObject go in _enemies)
        //foreach (Image go in _enemies)
        {
            //GameObject go = indicator.GameObj;            

            if (!go.gameObject.activeInHierarchy) return;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
            bool isOffscreen = !(screenPos.z > 0
                && screenPos.x > 0 && screenPos.x < Screen.width
                && screenPos.y > 0 && screenPos.y < Screen.height);
            //TMP
            isOffscreen = true;

            if (isOffscreen)
            {

                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

                GameObject enemyIndicator;               
                if (_tmpInstatiated == null)
                {
                    enemyIndicator = _enemyIndicatorPool.GetPooledObject();
                    enemyIndicator.SetActive(true);

                    enemyIndicator.transform.SetParent(_hudCavas.transform, false);     // false to scale image with Canvas

                    _tmpInstatiated = enemyIndicator;
                }
                else
                {
                    enemyIndicator = _tmpInstatiated;
                }

                Image enemyIndicatorImage = enemyIndicator.GetComponent<Image>();       // TODO: Make list with pooled objects, and remove GetComponent calls!!!
                //Image enemyIndicatorImage = go;

                // Adjust position for the image to fit the screen
                Vector2 imageOffset = ComputeImageOffset(enemyIndicatorImage, screenPos);
                Vector2 borderPosition = ComputeBorderPosition(go.transform.position);
                borderPosition += imageOffset;

                enemyIndicatorImage.rectTransform.anchoredPosition = borderPosition;

                return;
            }
        }
    }

    void UpdateScreenData()
    {
        _referenceResolution = _canvasScaler.referenceResolution;
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
    }

    Vector2 ComputeBorderPosition(Vector3 enemyPos)
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(enemyPos);

        //CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
        //float scaleX = scaler.referenceResolution.x / Screen.width;
        float scaleX = _referenceResolution.x / _screenWidth;

        //float scaleY = scaler.referenceResolution.y / Screen.height;
        float scaleY = scaleX;      // Scaler matches width, so only scale in X matters               

        Vector3 screenPoint = Camera.main.ViewportToScreenPoint(viewPos);
        Vector2 borderPosition = new Vector3(Mathf.Clamp(screenPoint.x, 0f, Screen.width), Mathf.Clamp(screenPoint.y, 0f, Screen.height), 0f);

        borderPosition.x -= Screen.width / 2;
        borderPosition.x *= scaleX;

        borderPosition.y -= Screen.height / 2;
        borderPosition.y *= scaleY;

        return borderPosition;
    }

    Vector2 ComputeImageOffset(Image indicatorImage, Vector3 screenPos)
    {
        Vector2 imageOffset;
        Vector2 imageSize = new Vector2(indicatorImage.rectTransform.rect.width, indicatorImage.rectTransform.rect.height);

        int offsetSignX = 0;
        int offsetSignY = 0;
        if (screenPos.x < 0)
        {
            offsetSignX = 1;
        }
        else if (screenPos.x > Screen.width)
        {
            offsetSignX = -1;
        }

        if (screenPos.y < 0)
        {
            offsetSignY = 1;
        }
        else if (screenPos.y > Screen.height)
        {
            offsetSignY = -1;
        }

        imageOffset = new Vector2(imageSize.x / 2, imageSize.y / 2);
        imageOffset.x *= offsetSignX;
        imageOffset.y *= offsetSignY;

        return imageOffset;
    }

    void AddEnemyToList(object sender, EnemyPropertiesEventArgs e)
    {
        if (!_enemies.Contains(e.EnemyGameObj))
        {
            _enemies.Add(e.EnemyGameObj);
            //_indicatorImages.Add(e.EnemyGameObj.GetComponent<Image>());

            //_enemyIndicators.Add(new EnemyIndicator(e.EnemyGameObj, e.EnemyGameObj.GetComponent<Image>()));
        }

        //if (!_enemyIndicators.Contains(e.EnemyGameObj))
        //{
        //    _enemyList.Add(e.EnemyGameObj);
        //}

        //Dictionary<GameObject, Image> dict = new Dictionary<GameObject, Image>();


    }
}

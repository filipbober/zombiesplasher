// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ZombieSplasher
{
    public class OffscreenEnemyIndicator : MonoBehaviour
    {
        [SerializeField]
        private Canvas _hudCavas;

        [SerializeField]
        private ObjectPooler _enemyIndicatorPool;

        private List<GameObject> _enemies;
        private List<GameObject> _activeIndicators;

        private CanvasScaler _canvasScaler;
        private Vector2 _referenceResolution;
        private int _screenWidth;
        //private int _screenHeight;

        void Awake()
        {
            _canvasScaler = _hudCavas.GetComponent<CanvasScaler>();
            _enemies = new List<GameObject>();
            _activeIndicators = new List<GameObject>();

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
            List<GameObject> offscreenEnemies = UpdateOffscreenEnemies();
            UpdateActiveIndicators(offscreenEnemies.Count);

            DrawIndicators(offscreenEnemies);
        }

        void DrawIndicators(List<GameObject> offscreenEnemies)
        {
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
        }

        void UpdateActiveIndicators(int offscreenEnemiesCount)
        {
            foreach (GameObject indicator in _activeIndicators)
            {
                indicator.gameObject.SetActive(false);
            }

            _activeIndicators = new List<GameObject>(offscreenEnemiesCount);
        }

        List<GameObject> UpdateOffscreenEnemies()
        {
            List<GameObject> offscreenEnemies;
            offscreenEnemies = new List<GameObject>(_enemies.Count);

            foreach (GameObject go in _enemies)
            {
                if (go.gameObject.activeInHierarchy)
                {

                    Vector3 screenPos = Camera.main.WorldToScreenPoint(go.transform.position);
                    bool isOffscreen = !(screenPos.z > 0
                        && screenPos.x > 0 && screenPos.x < Screen.width
                        && screenPos.y > 0 && screenPos.y < Screen.height);

                    if (isOffscreen && !offscreenEnemies.Contains(go))
                    {
                        offscreenEnemies.Add(go);
                    }
                }

            }

            return offscreenEnemies;
        }

        void UpdateScreenData()
        {
            _referenceResolution = _canvasScaler.referenceResolution;
            _screenWidth = Screen.width;
            //_screenHeight = Screen.height;
        }

        Vector2 ComputeBorderPosition(Vector3 enemyPos)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(enemyPos);
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

        void AddEnemyToList(object sender, ActorPropertiesEventArgs e)
        {
            if (!_enemies.Contains(e.Sender))
            {
                _enemies.Add(e.Sender);
            }
        }

    }
}

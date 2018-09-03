// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace FCB.BackgroundRender
{
    public class RenderTextureCreator : MonoBehaviour
    {
        private RenderTexture _renderTexture;

        public RenderTexture GetCameraViewTexture()
        {
            if (_renderTexture == null)
            {
                CreateRenderTexture();
            }

            Debug.Log("RenderTexture");
            return _renderTexture;
        }

        private void CreateRenderTexture()
        {
            Camera camera = GetComponent<Camera>();
            camera.depthTextureMode = DepthTextureMode.Depth;

            int resWidth = Screen.width;
            int resHeight = Screen.height;
            Debug.Log("Width = " + Screen.width);
            Debug.Log("Height = " + Screen.height);

            RenderTexture renderTexture = new RenderTexture(resWidth, resHeight, 24, RenderTextureFormat.ARGBFloat);
            renderTexture.antiAliasing = 1;
            renderTexture.filterMode = FilterMode.Point;
            renderTexture.anisoLevel = 9;
            renderTexture.autoGenerateMips = false;
            //Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

            //renderTexture.width = resWidth;
            //renderTexture.height = resHeight;

            camera.targetTexture = renderTexture;       //Create new renderTexture and assign to camera
            
            _renderTexture = renderTexture;
        }

    }
}

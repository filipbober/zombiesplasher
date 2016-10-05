// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

public class RenderTextureCreator : MonoBehaviour
{
    private RenderTexture _renderTexture;

    public RenderTexture GetCameraViewTexture()
    {
        if (_renderTexture == null)
        {
            CreateRenderTexture();
        }

        return _renderTexture;
    }

    private void CreateRenderTexture()
    {
        Camera camera = GetComponent<Camera>();

        int resWidth = Screen.width;
        int resHeight = Screen.height;

        RenderTexture renderTexture = new RenderTexture(resWidth, resHeight, 24);
        
        //Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

        renderTexture.width = resWidth;
        renderTexture.height = resHeight;

        camera.targetTexture = renderTexture;       //Create new renderTexture and assign to camera
        _renderTexture = renderTexture;
    }

}

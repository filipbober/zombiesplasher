// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

[ExecuteInEditMode]
public class RenderTextureCreator : MonoBehaviour
{
    // Before SetCameraShader, because it uses render textures definded here
    void Awake()
    {
        Camera camera = GetComponent<Camera>();

        int resWidth = Screen.width;
        int resHeight = Screen.height;

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt; //Create new renderTexture and assign to camera
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

        GetComponent<Camera>().targetTexture.width = resHeight;
        GetComponent<Camera>().targetTexture.height = resHeight;
    }
}

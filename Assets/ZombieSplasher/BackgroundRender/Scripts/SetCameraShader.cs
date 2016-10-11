// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace FCB.BackgroundRender
{
    [ExecuteInEditMode]
    public class SetCameraShader : MonoBehaviour
    {
        [SerializeField]
        private RenderTextureCreator _colorCamera;

        [SerializeField]
        private RenderTextureCreator _depthCamera;

        [SerializeField]
        private Material _backgroundRenderMat;

        private readonly string ColorCameraView = ShaderConfig.BackgroundRenderShader.ColorCameraView;
        private readonly string DepthCameraView = ShaderConfig.BackgroundRenderShader.DepthCameraView;

        void Start()
        {
            _backgroundRenderMat.SetTexture(ColorCameraView, _colorCamera.GetCameraViewTexture());
            _backgroundRenderMat.SetTexture(DepthCameraView, _depthCamera.GetCameraViewTexture());
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(null, destination, _backgroundRenderMat);
        }

    }
}

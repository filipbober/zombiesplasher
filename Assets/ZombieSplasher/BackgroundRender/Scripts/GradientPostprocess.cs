// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace FCB.BackgroundRender
{
    [ExecuteInEditMode]
    public class GradientPostprocess : MonoBehaviour
    {
        [SerializeField]
        private Material _backgroundRenderMat;

        [SerializeField]
        private Material _depthGradientMat;

        [SerializeField]
        private RenderTextureCreator _colorCamera;

        [SerializeField]
        private RenderTextureCreator _depthCamera;

        [SerializeField]
        private RenderTextureCreator _shaderCamera;

        [SerializeField]
        private RenderTextureCreator _3dDepthCamera;

        [SerializeField]
        private float _gradientValue = ShaderConfig.GradientShader.GradientDefaultValue;

        bool showNormalColors = false;

        private readonly string BgColorTextureProperty = ShaderConfig.GradientShader.BgColorReference;
        private readonly string BgDepthTextureProperty = ShaderConfig.GradientShader.BgDepthReference;
        private readonly string BgShaderDepthTextureReference = ShaderConfig.GradientShader.BgShaderDepthTextureReference;

        private readonly string GradientProperty = ShaderConfig.GradientShader.GradientReference;

        private readonly string DepthColorViewProperty = ShaderConfig.GradientShader.DepthColorView;
        private readonly float DepthViewOn = ShaderConfig.GradientShader.DepthViewOn;
        private readonly float DepthViewOff = ShaderConfig.GradientShader.DepthViewOff;

        void Start()
        {
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;

            //_depthGradientMat.SetTexture(BgColorTextureProperty, _colorCamera.GetCameraViewTexture());
            //_depthGradientMat.SetTexture(BgDepthTextureProperty, _depthCamera.GetCameraViewTexture());
            _depthGradientMat.SetTexture(BgColorTextureProperty, _colorCamera.GetCameraViewTexture());
            _depthGradientMat.SetTexture(BgDepthTextureProperty, _depthCamera.GetCameraViewTexture());
            _depthGradientMat.SetTexture(BgShaderDepthTextureReference, _depthCamera.GetCameraViewTexture());
            _depthGradientMat.SetFloat(GradientProperty, _gradientValue);

            // ---
            _depthGradientMat.SetTexture("_3dDepth", _3dDepthCamera.GetCameraViewTexture());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                showNormalColors = !showNormalColors;
            }

            if (showNormalColors)
            {
                _depthGradientMat.SetFloat(DepthColorViewProperty, DepthViewOn);
            }
            else
            {
                _depthGradientMat.SetFloat(DepthColorViewProperty, DepthViewOff);
            }

            
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {

            //RenderTexture dst = new RenderTexture(source.width, source.height, source.depth);
            //Graphics.Blit(source, dst, _backgroundRenderMat);
            //Graphics.Blit(dst, destination, _depthGradientMat);

            Graphics.Blit(source, destination, _depthGradientMat);
            // ---
            //var height = _depthGradientMat.GetFloat("_Gradient");
            //Debug.Log(height);
            // ---
        }
    }
}
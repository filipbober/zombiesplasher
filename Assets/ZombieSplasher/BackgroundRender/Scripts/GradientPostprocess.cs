// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace FCB.BackgroundRender
{
    [ExecuteInEditMode]
    public class GradientPostprocess : MonoBehaviour
    {
        //[SerializeField]
        //private Material _backgroundRenderMat;


        //[SerializeField]
        //private Material _depthGradientMat;

        [SerializeField]
        private Shader _depthGradientShader;

        [SerializeField]
        private RenderTextureCreator _bgColor2dCamera;

        [SerializeField]
        private RenderTextureCreator _bgDepth2dCamera;

        [SerializeField]
        private RenderTextureCreator _actorsDepthCamera;

        [SerializeField]
        private RenderTextureCreator _envColorCamera;

        [SerializeField]
        private RenderTextureCreator _envDepthCamera;

        //[SerializeField]
        //private RenderTextureCreator _2dBgColorCamera;

        //[SerializeField]
        //private RenderTextureCreator _2dBgDepthCamera;

            

        //[SerializeField]
        //private float _gradientValue = ShaderConfig.GradientShader.GradientDefaultValue;

        bool showNormalColors = false;

        private readonly string BgColorTextureProperty = ShaderConfig.GradientShader.BgColorReference;
        private readonly string BgDepthTextureProperty = ShaderConfig.GradientShader.BgDepthReference;
        private readonly string BgShaderDepthTextureReference = ShaderConfig.GradientShader.BgShaderDepthTextureReference;

        private readonly string GradientProperty = ShaderConfig.GradientShader.GradientReference;

        private readonly string DepthColorViewProperty = ShaderConfig.GradientShader.DepthColorView;
        private readonly float DepthViewOn = ShaderConfig.GradientShader.DepthViewOn;
        private readonly float DepthViewOff = ShaderConfig.GradientShader.DepthViewOff;

        //private Camera _attachedCamera;
        //private Camera _tempCam;
        //private Material _postMat;
        //private Material _depthGradientMat;
        //private Material _backgroundRenderMat;
        private Material _depthGradientMat;



        //[SerializeField]
        //private Shader _postOutline;

        //[SerializeField]
        //private Shader _drawSimple;

        void Start()
        {
            //_attachedCamera = GetComponent<Camera>();
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
            //_tempCam = new GameObject().AddComponent<Camera>();
            //_tempCam.enabled = false;
            //_postMat = new Material(_postOutline);
            _depthGradientMat = new Material(_depthGradientShader);

            //_depthGradientMat.SetTexture(BgColorTextureProperty, _colorCamera.GetCameraViewTexture());
            //_depthGradientMat.SetTexture(BgDepthTextureProperty, _depthCamera.GetCameraViewTexture());
            _depthGradientMat.SetTexture("_bgColor", _bgColor2dCamera.GetCameraViewTexture());
            _depthGradientMat.SetTexture("_bgDepth", _bgDepth2dCamera.GetCameraViewTexture());

            //_depthGradientMat.SetTexture("_BgShaderDepth", _2dEnvDepthCamera.GetCameraViewTexture());
            //_depthGradientMat.SetFloat(GradientProperty, _gradientValue);

            // ---
            _depthGradientMat.SetTexture("_actorsDepth", _actorsDepthCamera.GetCameraViewTexture());

            _depthGradientMat.SetTexture("_envColor", _envColorCamera.GetCameraViewTexture());
            _depthGradientMat.SetTexture("_envDepth", _envDepthCamera.GetCameraViewTexture());

            //_depthGradientMat.SetTexture("_2dBgColor", _2dBgColorCamera.GetCameraViewTexture());
            //_depthGradientMat.SetTexture("_2dBgDepth", _2dBgDepthCamera.GetCameraViewTexture());
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

            //Debug.Log("Clear");
            //GL.Clear(true, true, new Color(0f, 0f, 0f, 1f), 1f);
            //Graphics.Blit(source, destination, _depthGradientMat);


            //RenderTexture tmpDestination = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);
            //Graphics.Blit(source, tmpDestination, _depthGradientMat);
            Graphics.Blit(source, null, _depthGradientMat);

            //Graphics.Blit(tmpDestination, destination, _depthGradientMat);


            // ---
            //var height = _depthGradientMat.GetFloat("_Gradient");
            //Debug.Log(height);
            // ---


            ////set up a temporary camera
            //_tempCam.CopyFrom(_attachedCamera);
            //_tempCam.clearFlags = CameraClearFlags.Color;
            //_tempCam.backgroundColor = Color.black;

            ////cull any layer that isn't the outline
            //_tempCam.cullingMask = 1 << LayerMask.NameToLayer("3dTransparent");

            ////make the temporary rendertexture
            //RenderTexture TempRT = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);

            ////put it to video memory
            //TempRT.Create();

            ////set the camera's target texture when rendering
            //_tempCam.targetTexture = TempRT;

            ////render all objects this camera can render, but with our custom shader.
            //_tempCam.RenderWithShader(_depthGradientShader, "");

            ////copy the temporary RT to the final image
            //Graphics.Blit(TempRT, destination, _postMat);

            ////release the temporary RT
            //TempRT.Release();



        }

        void OnPostRender()
        {

        }


    }
}
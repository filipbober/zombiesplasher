using UnityEngine;
using System.Collections;
//http://www.alanzucconi.com/2015/07/08/screen-shaders-and-postprocessing-effects-in-unity3d/

[ExecuteInEditMode]
public class DepthNormals : MonoBehaviour
{

    public Material mat;
    bool showNormalColors = true;

    [SerializeField]
    Camera _colorCamera;

    [SerializeField]
    Camera _depthCamera;

    [SerializeField]
    float _gradientValue = ShaderConfig.GradientShader.GradientDefaultValue;

    private readonly string BgColorTextureProperty = ShaderConfig.GradientShader.BgColorReference;
    private readonly string BgDepthTextureProperty = ShaderConfig.GradientShader.BgDepthReference;

    private readonly string GradientProperty = ShaderConfig.GradientShader.GradientReference;
    //private readonly float GradientValue = ShaderConfig.GradientShader.GradientDefaultValue;

    private readonly string DepthColorViewProperty = ShaderConfig.GradientShader.DepthColorView;
    private readonly float DepthViewOn = ShaderConfig.GradientShader.DepthViewOn;
    private readonly float DepthViewOff = ShaderConfig.GradientShader.DepthViewOff;

    void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;

        Camera camera = GetComponent<Camera>();
        mat.SetTexture(BgColorTextureProperty, _colorCamera.targetTexture);
        mat.SetTexture(BgDepthTextureProperty, _depthCamera.targetTexture);

        //mat.SetFloat(GradientProperty, _gradientValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            showNormalColors = !showNormalColors;
        }

        if (showNormalColors)
        {
            mat.SetFloat(DepthColorViewProperty, DepthViewOn);
        }
        else
        {
            mat.SetFloat(DepthColorViewProperty, DepthViewOff);
        }
    }

    // Called by the camera to apply the image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //mat is the material containing your shader
        Graphics.Blit(source, destination, mat);
    }

    //void OnPostRender()
    //{
    //    Graphics.Blit(null, destination, mat);
    //}
}
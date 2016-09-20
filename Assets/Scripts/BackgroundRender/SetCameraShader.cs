using UnityEngine;

//http://gamedev.stackexchange.com/questions/108391/unity3d-override-main-camera-rendering-for-compositing-effect

[ExecuteInEditMode]
public class SetCameraShader : MonoBehaviour
{
    [SerializeField]
    Camera _colorCamera;

    [SerializeField]
    Camera _depthCamera;

    public Material mat;

    private readonly string ColorCameraView = ShaderConfig.BackgroundRenderShader.ColorCameraView;
    private readonly string DepthCameraView = ShaderConfig.BackgroundRenderShader.DepthCameraView;

    void Start()
    {
        mat.SetTexture(ColorCameraView, _colorCamera.targetTexture);
        mat.SetTexture(DepthCameraView, _depthCamera.targetTexture);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(null, destination, mat);
    }

}

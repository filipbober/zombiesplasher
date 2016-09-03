using UnityEngine;
using System.Collections;

//http://gamedev.stackexchange.com/questions/108391/unity3d-override-main-camera-rendering-for-compositing-effect

public class SetCameraShader : MonoBehaviour
{
    [SerializeField]
    Camera _colorCamera;

    [SerializeField]
    Camera _depthCamera;

    //[SerializeField]
    //Camera _camera;

    //[SerializeField]
    //Shader _shader;

    public Material mat;


    // TODO:
    // Create RenderTexture manually and adjust its size to screen size


    // Use this for initialization
    void Start()
    {
        //Camera.main.SetReplacementShader(Shader.Find("Your Shader"), "RenderType")
        //GetComponent<Renderer>().material.SetTexture(

        //_camera.SetReplacementShader(_shader, "RenderType");

        //mat.SetTexture("_RGB", _colorCamera.targetTexture);
        //mat.SetTexture("_DEPTH", _depthCamera.targetTexture);

        mat.SetTexture("_RGB", _colorCamera.targetTexture);
        mat.SetTexture("_DEPTH", _depthCamera.targetTexture);
    }

    void OnPostRender()
    {
        //_camera.SetReplacementShader(_shader, "RenderType");
    }

    // Update is called once per frame
    void Update()
    {
        //_camera.SetReplacementShader(_shader, "RenderType");
        //mat.SetTexture("_RGB", _colorCamera.targetTexture);
        //mat.SetTexture("_DEPTH", _depthCamera.targetTexture);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)

    {

        //Graphics.Blit(null, destination, effectMaterial);
        Graphics.Blit(null, destination, mat);
    }

}

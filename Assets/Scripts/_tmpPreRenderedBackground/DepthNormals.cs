using UnityEngine;
using System.Collections;
//http://www.alanzucconi.com/2015/07/08/screen-shaders-and-postprocessing-effects-in-unity3d/
public class DepthNormals : MonoBehaviour
{

    public Material mat;
    bool showNormalColors = true;

    [SerializeField]
    Camera _colorCamera;

    [SerializeField]
    Camera _depthCamera;

    void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;


        Camera camera = GetComponent<Camera>();
        mat.SetTexture("_RGB", _colorCamera.targetTexture);
        mat.SetTexture("_DEPTH", _depthCamera.targetTexture);
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
            mat.SetFloat("_showNormalColors", 1.0f);
        }
        else
        {
            mat.SetFloat("_showNormalColors", 0.0f);
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
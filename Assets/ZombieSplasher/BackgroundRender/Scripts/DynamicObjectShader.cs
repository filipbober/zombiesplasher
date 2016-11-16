using UnityEngine;

[ExecuteInEditMode]
public class DynamicObjectShader : MonoBehaviour
{
    [SerializeField]
    private Shader _shader;

    protected void Awake()
    {
        if (_shader)
            GetComponent<Camera>().SetReplacementShader(_shader, null);
    }

}
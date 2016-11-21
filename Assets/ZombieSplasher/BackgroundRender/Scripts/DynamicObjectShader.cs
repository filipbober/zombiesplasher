using UnityEngine;

[ExecuteInEditMode]
public class DynamicObjectShader : MonoBehaviour
{
    [SerializeField]
    private Shader _shader;

    [SerializeField]
    float _div = 1;

    protected void Awake()
    {
        if (_shader)
            GetComponent<Camera>().SetReplacementShader(_shader, null);
    }

}
using UnityEngine;

public class TerrainSettings : MonoBehaviour
{
    public float SpeedModifier { get { return _speedModifier; } }

    [SerializeField]
    private float _speedModifier = 1.0f;
}

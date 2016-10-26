using UnityEngine;

public class TerrainExitEvent : FCB.EventSystem.GameEvent, FCB.EventSystem.ISingleEvent
{
    public readonly GameObject Sender;

    public TerrainExitEvent(GameObject sender)
    {
        Sender = sender;
    }
}

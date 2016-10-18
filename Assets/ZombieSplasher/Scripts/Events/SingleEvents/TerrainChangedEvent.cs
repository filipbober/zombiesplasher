using UnityEngine;

namespace ZombieSplasher
{
    public class TerrainChangedEvent : FCB.EventSystem.GameEvent, FCB.EventSystem.ISingleEvent
    {
        public readonly GameObject Sender;
        public readonly float SpeedModifier;

        public TerrainChangedEvent(GameObject sender, float speedModifier)
        {
            Sender = sender;
            SpeedModifier = speedModifier;
        }
    }
}

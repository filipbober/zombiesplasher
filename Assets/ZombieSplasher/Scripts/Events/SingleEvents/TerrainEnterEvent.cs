using UnityEngine;

namespace ZombieSplasher
{
    public class TerrainEnterEvent : FCB.EventSystem.GameEvent, FCB.EventSystem.ISingleEvent
    {
        public readonly GameObject Sender;
        public readonly float SpeedModifier;

        public TerrainEnterEvent(GameObject sender, float speedModifier)
        {
            Sender = sender;
            SpeedModifier = speedModifier;
        }
    }
}

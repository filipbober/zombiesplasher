using UnityEngine;

namespace ZombieSplasher
{
    public class SpawnCorpseEvent : ActorPropertiesEvent
    {
        public SpawnCorpseEvent(GameObject sender, ActorProperties actorProperties) : base(sender, actorProperties)
        {
        }
    }
}

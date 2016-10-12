using UnityEngine;

namespace ZombieSplasher
{
    public class ActorPropertiesEventArgs : System.EventArgs
    {

        public readonly GameObject Sender;
        public readonly ActorProperties ActorProperties;

        public ActorPropertiesEventArgs(GameObject sender, ActorProperties actorProperties)
        {
            Sender = sender;
            ActorProperties = actorProperties;
        }
    }
}

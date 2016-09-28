using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class ActorLookup : MonoBehaviour
{
    public Dictionary<Enums.ActorType, GameObject> Actors = new Dictionary<Enums.ActorType, GameObject>();

    [SerializeField]
    private List<GameObject> _actors;

    void Awake()
    {
        foreach (var actor in _actors)
        {
            ActorProperties properties = actor.GetComponent<ActorProperties>();
            GameObject newActor;
            if (!Actors.TryGetValue(properties.ActorType, out newActor))
            {
                Actors.Add(properties.ActorType, actor);
            }            
        }

        Debug.Log("Actors created = " + Actors.Count);
    }

}

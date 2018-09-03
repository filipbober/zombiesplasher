using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public interface IInteractable
    {
        Utility ComputeBestUtility(InteractableObject actor);
    }
}

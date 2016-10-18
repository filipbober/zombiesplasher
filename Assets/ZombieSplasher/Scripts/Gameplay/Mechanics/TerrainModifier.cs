using System;
using UnityEngine;

namespace ZombieSplasher
{
    public class TerrainModifier : MonoBehaviour
    {
        [SerializeField]
        private float _speedModifier = 1.0f;

        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.Enemy))
            {
                Debug.Log("Trigger entered!");
                IEnemyMover mover = other.GetComponent<IEnemyMover>();
                mover.SetSpeed(mover.DefaultSpeed * _speedModifier);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameTags.Enemy))
            {
                IEnemyMover mover = other.GetComponent<IEnemyMover>();
                mover.SetSpeed(mover.DefaultSpeed);
            }
        }
    }
}

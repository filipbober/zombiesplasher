using UnityEngine;

namespace AI
{
    public class ShopInteract : MonoBehaviour, IInteractable
    {
        public Utility ComputeBestUtility(InteractableObject actor)
        {
            var repair = ComputeRepairUtility(actor);
            var trade = ComputeTradeUtility(actor);

            if (repair > trade)
                return new Utility(Interaction.Repair, repair);
            else
                return new Utility(Interaction.Trade, trade);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private float ComputeRepairUtility(InteractableObject actor)
        {
            return Random.Range(0.1f, 0.99f);
        }

        private float ComputeTradeUtility(InteractableObject actor)
        {
            return Random.Range(0.1f, 0.99f);
        }

    }
}

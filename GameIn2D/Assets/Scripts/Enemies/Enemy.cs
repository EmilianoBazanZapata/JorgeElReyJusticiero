using UnityEngine;

namespace GameIn2D.Enemies
{

    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        private bool turnAround;
        private int lastTurnFrame = -1;

        public bool TurnAround => turnAround;

        public void SetTurnAround(bool shouldTurnAround)
        {
            turnAround = shouldTurnAround;
        }

        public void ReverseDirection()
        {
            // An enemy can have more than one trigger collider. Unity may report
            // the same limit contact to each one during the same frame.
            if (lastTurnFrame == Time.frameCount)
            {
                return;
            }

            turnAround = !turnAround;
            lastTurnFrame = Time.frameCount;
        }
    }
}

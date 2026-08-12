using UnityEngine;

namespace GameIn2D.Enemies
{

    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        private bool turnAround;

        public bool TurnAround => turnAround;

        public void SetTurnAround(bool shouldTurnAround)
        {
            turnAround = shouldTurnAround;
        }
    }
}

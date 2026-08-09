using UnityEngine;

namespace GameIn2D.Enemies
{

    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        public static bool turnAround;

        public static void SetTurnAround(bool shouldTurnAround)
        {
            turnAround = shouldTurnAround;
        }
    }
}

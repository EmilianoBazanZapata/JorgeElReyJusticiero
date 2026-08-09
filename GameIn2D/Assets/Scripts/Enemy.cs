using UnityEngine;

[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    public static bool turnAround;

    public static void SetTurnAround(bool shouldTurnAround)
    {
        turnAround = shouldTurnAround;
    }
}

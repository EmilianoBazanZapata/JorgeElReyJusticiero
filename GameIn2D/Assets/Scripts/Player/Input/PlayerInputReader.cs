using UnityEngine;

namespace GameIn2D.Player.Input
{

    [DisallowMultipleComponent]
    public sealed class PlayerInputReader : MonoBehaviour
    {
        public bool JumpPressed { get; private set; }
        public bool SuperRunPressed { get; private set; }

        public void ReadInput()
        {
            JumpPressed = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            SuperRunPressed = UnityEngine.Input.GetButton("SuperRun");
        }

        public float ReadHorizontalMovement()
        {
            return UnityEngine.Input.GetAxisRaw("HorizontalMove");
        }
    }
}

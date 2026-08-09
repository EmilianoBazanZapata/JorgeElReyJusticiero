using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerInputReader : MonoBehaviour
{
    public bool JumpPressed { get; private set; }
    public bool SuperRunPressed { get; private set; }

    public void ReadInput()
    {
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        SuperRunPressed = Input.GetButton("SuperRun");
    }

    public float ReadHorizontalMovement()
    {
        return Input.GetAxisRaw("HorizontalMove");
    }
}

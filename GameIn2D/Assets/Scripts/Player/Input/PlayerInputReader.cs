using UnityEngine;
using UnityEngine.InputSystem;

namespace GameIn2D.Player.Input
{

    [DisallowMultipleComponent]
    public sealed class PlayerInputReader : MonoBehaviour
    {
        private const float MoveDeadZone = 0.2f;

        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction superRunAction;

        public bool JumpPressed { get; private set; }
        public bool SuperRunPressed { get; private set; }

        private void Awake()
        {
            moveAction = new InputAction("Move", InputActionType.Value);
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Keyboard>/a")
                .With("Negative", "<Keyboard>/leftArrow")
                .With("Positive", "<Keyboard>/d")
                .With("Positive", "<Keyboard>/rightArrow");
            moveAction.AddBinding("<Gamepad>/leftStick/x");
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Gamepad>/dpad/left")
                .With("Positive", "<Gamepad>/dpad/right");

            jumpAction = new InputAction("Jump", InputActionType.Button, "<Keyboard>/space");
            jumpAction.AddBinding("<Gamepad>/buttonSouth");
            superRunAction = new InputAction("SuperRun", InputActionType.Button, "<Keyboard>/leftShift");
            superRunAction.AddBinding("<Gamepad>/rightShoulder");
        }

        private void OnEnable()
        {
            moveAction.Enable();
            jumpAction.Enable();
            superRunAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            jumpAction.Disable();
            superRunAction.Disable();
        }

        private void OnDestroy()
        {
            moveAction.Dispose();
            jumpAction.Dispose();
            superRunAction.Dispose();
        }

        public void ReadInput()
        {
            JumpPressed = jumpAction.WasPressedThisFrame();
            SuperRunPressed = superRunAction.IsPressed();
        }

        public float ReadHorizontalMovement()
        {
            float horizontalMovement = moveAction.ReadValue<float>();
            return Mathf.Abs(horizontalMovement) < MoveDeadZone
                ? 0f
                : Mathf.Sign(horizontalMovement);
        }
    }
}

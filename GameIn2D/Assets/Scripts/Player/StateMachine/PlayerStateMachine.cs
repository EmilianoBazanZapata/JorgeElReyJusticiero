using UnityEngine;
using GameIn2D.Player.Input;
using GameIn2D.Player.Lifecycle;
using GameIn2D.Player.Movement;

namespace GameIn2D.Player.StateMachine
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInputReader), typeof(PlayerMovement), typeof(PlayerDeathController))]
    public sealed class PlayerStateMachine : MonoBehaviour
    {
        private PlayerInputReader inputReader;
        private PlayerMovement playerMovement;
        private PlayerDeathController playerDeathController;

        private IPlayerState idleState;
        private IPlayerState runState;
        private IPlayerState jumpState;
        private IPlayerState fallState;
        private IPlayerState deadState;
        private IPlayerState currentState;

        public PlayerStateId CurrentState => currentState.Id;

        private void Awake()
        {
            inputReader = GetComponent<PlayerInputReader>();
            playerMovement = GetComponent<PlayerMovement>();
            playerDeathController = GetComponent<PlayerDeathController>();

            idleState = new PlayerIdleState();
            runState = new PlayerRunState();
            jumpState = new PlayerJumpState();
            fallState = new PlayerFallState();
            deadState = new PlayerDeadState();
            currentState = idleState;
        }

        private void OnEnable()
        {
            playerDeathController.Died += EnterDeadState;
        }

        private void OnDisable()
        {
            playerDeathController.Died -= EnterDeadState;
        }

        public void Tick()
        {
            if (CurrentState == PlayerStateId.Dead)
            {
                return;
            }

            inputReader.ReadInput();
            playerMovement.Tick();
            EvaluateAirborneState();
        }

        public void FixedTick()
        {
            if (CurrentState == PlayerStateId.Dead)
            {
                return;
            }

            float horizontalMovement = inputReader.ReadHorizontalMovement();
            playerMovement.FixedTick(horizontalMovement);
            EvaluateLocomotionState();
        }

        public void ResetState()
        {
            ChangeState(PlayerStateId.Idle);
        }

        private void EvaluateAirborneState()
        {
            if (!playerMovement.IsGrounded)
            {
                ChangeState(playerMovement.VerticalVelocity > 0f
                    ? PlayerStateId.Jump
                    : PlayerStateId.Fall);
            }
        }

        private void EvaluateLocomotionState()
        {
            if (!playerMovement.IsGrounded)
            {
                ChangeState(playerMovement.VerticalVelocity > 0f
                    ? PlayerStateId.Jump
                    : PlayerStateId.Fall);
                return;
            }

            ChangeState(playerMovement.IsMoving
                ? PlayerStateId.Run
                : PlayerStateId.Idle);
        }

        private void EnterDeadState()
        {
            playerMovement.ResetJumpState();
            ChangeState(PlayerStateId.Dead);
        }

        private void ChangeState(PlayerStateId nextStateId)
        {
            if (currentState.Id == nextStateId || !currentState.CanTransitionTo(nextStateId))
            {
                return;
            }

            currentState = GetState(nextStateId);
        }

        private IPlayerState GetState(PlayerStateId stateId)
        {
            switch (stateId)
            {
                case PlayerStateId.Idle:
                    return idleState;
                case PlayerStateId.Run:
                    return runState;
                case PlayerStateId.Jump:
                    return jumpState;
                case PlayerStateId.Fall:
                    return fallState;
                case PlayerStateId.Dead:
                    return deadState;
                default:
                    return idleState;
            }
        }
    }
}

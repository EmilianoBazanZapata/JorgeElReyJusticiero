namespace GameIn2D.Player.StateMachine
{

    public sealed class PlayerIdleState : IPlayerState
    {
        public PlayerStateId Id => PlayerStateId.Idle;

        public bool CanTransitionTo(PlayerStateId nextState)
        {
            return nextState == PlayerStateId.Run ||
                   nextState == PlayerStateId.Jump ||
                   nextState == PlayerStateId.Fall ||
                   nextState == PlayerStateId.Dead;
        }
    }
}

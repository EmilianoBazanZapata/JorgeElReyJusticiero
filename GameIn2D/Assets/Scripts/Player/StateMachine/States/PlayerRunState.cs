namespace GameIn2D.Player.StateMachine
{

    public sealed class PlayerRunState : IPlayerState
    {
        public PlayerStateId Id => PlayerStateId.Run;

        public bool CanTransitionTo(PlayerStateId nextState)
        {
            return nextState == PlayerStateId.Idle ||
                   nextState == PlayerStateId.Jump ||
                   nextState == PlayerStateId.Fall ||
                   nextState == PlayerStateId.Dead;
        }
    }
}

namespace GameIn2D.Player.StateMachine
{

    public sealed class PlayerDeadState : IPlayerState
    {
        public PlayerStateId Id => PlayerStateId.Dead;

        public bool CanTransitionTo(PlayerStateId nextState)
        {
            return nextState == PlayerStateId.Idle;
        }
    }
}

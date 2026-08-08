public sealed class PlayerFallState : IPlayerState
{
    public PlayerStateId Id => PlayerStateId.Fall;

    public bool CanTransitionTo(PlayerStateId nextState)
    {
        return nextState == PlayerStateId.Jump ||
               nextState == PlayerStateId.Idle ||
               nextState == PlayerStateId.Run ||
               nextState == PlayerStateId.Dead;
    }
}

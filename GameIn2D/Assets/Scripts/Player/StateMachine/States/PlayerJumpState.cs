public sealed class PlayerJumpState : IPlayerState
{
    public PlayerStateId Id => PlayerStateId.Jump;

    public bool CanTransitionTo(PlayerStateId nextState)
    {
        return nextState == PlayerStateId.Fall ||
               nextState == PlayerStateId.Idle ||
               nextState == PlayerStateId.Run ||
               nextState == PlayerStateId.Dead;
    }
}

public enum PlayerStateId
{
    Idle,
    Run,
    Jump,
    Fall,
    Dead
}

public interface IPlayerState
{
    PlayerStateId Id { get; }
    bool CanTransitionTo(PlayerStateId nextState);
}

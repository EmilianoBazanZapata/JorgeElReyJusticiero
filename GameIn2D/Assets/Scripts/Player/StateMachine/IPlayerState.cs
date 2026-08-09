namespace GameIn2D.Player.StateMachine
{

    public interface IPlayerState
    {
        PlayerStateId Id { get; }
        bool CanTransitionTo(PlayerStateId nextState);
    }
}

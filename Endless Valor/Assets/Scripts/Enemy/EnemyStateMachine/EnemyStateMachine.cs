
public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    
    public EnemyState PreviousState { get; private set; }

    public void Initialize(EnemyState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    
    public void ChangeState(EnemyState newState)
    {
        PreviousState = CurrentState;
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    

}

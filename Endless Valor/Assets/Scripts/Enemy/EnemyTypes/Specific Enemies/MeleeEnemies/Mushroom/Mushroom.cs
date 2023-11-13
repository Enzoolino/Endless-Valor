using UnityEngine;

public class Mushroom : MeleeEnemy
{
    public Mushroom_IdleState IdleState { get; private set; }
    public Mushroom_MoveState MoveState { get; private set; }


    [SerializeField] private D_EnemyIdleState idleStateData;
    [SerializeField] private D_EnemyMoveState moveStateData;

    public override void Start()
    {
        base.Start();
        IdleState = new Mushroom_IdleState(this, StateMachine, "isIdle", idleStateData, this);
        MoveState = new Mushroom_MoveState(this, StateMachine, "isWalking", moveStateData, this); //TODO: Change the animation name

    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class D_Player : ScriptableObject
{
    [Header("General")] 
    public float maxHealth = 100.0f;
    
    [Header("Move State")] 
    public float baseMovementSpeed = 4.0f;
    public float maximumMovementSpeed = 20.0f;
    public float acceleration = 0.5f;
    public float triggerEffectMovementSpeed = 10.0f;

    [Header("Jump State")] 
    public float baseJumpHeight = 6.0f;
    public float maximumJumpHeight = 15.0f;
    public float triggerEffectJumpHeight = 10.0f;
    public int amountOfJumps = 1;

    [Header("In Air State")] 
    public float smallFallDamageVelocity = 9.0f;
    public float mediumFallDamageVelocity = 14.0f;
    public float hugeFallDamageVelocity = 20.0f;
    public float jumpHeightMultiplier = 0.5f;

    [Header("Wall Grab State")] 
    public int amountOfGrabs = 1;
    public float timeToStartSliding = 1.0f;

    [Header("Wall Jump State")] 
    public float wallJumpVelocity = 7.0f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    
    [Header("Wall Slide State")] 
    public float slideSpeed = 1.0f;

    [Header("Ledge Climb State")] 
    public float grabLedgeDelayTime = 1.0f;
    public float timeToEnableMovingAfterLedgeJump = 1.0f;
    public float ledgeJumpForce = 7.0f;
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Attack")] 
    public float comboTimer = 3.0f;

    [Header("Primary Attack State")] 
    public float primaryAttackDamage = 25.0f;
    public float primaryAttackStunDamage = 1.0f;

    [Header("Secondary Attack State")] 
    public float secondaryAttackDamage = 30.0f;
    public float secondaryAttackStunDamage = 1.0f;

    [Header("Finisher Attack State")] 
    public float finishingAttackDamage = 30.0f;
    public float finishingAttackStunDamage = 1.0f;

    [Header("Climb Ladder State")] 
    public float climbLadderSpeed = 4.0f;

    [Header("Dead State")] 
    public float deathScreenDelay = 5.0f;
    
    [Header("Check Variables")] 
    public float groundCheckRange = 0.05f;
    public float wallCheckRange = 0.5f;
    public float ledgeCheckRange = 0.7f;

    [Header("Layer Masks")] 
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public LayerMask enemyLayerMask;


}

using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class D_Player : ScriptableObject
{
    [Header("Move State")] 
    public float movementSpeed = 4.0f;

    [Header("Jump State")] 
    public float jumpVelocity = 6.0f;
    public int amountOfJumps = 1;

    [Header("In Air State")] 
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
    
    
    [Header("Check Variables")] 
    public float groundCheckRange = 0.05f;
    public float wallCheckRange = 0.5f;

    [Header("Layer Masks")] 
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    

}

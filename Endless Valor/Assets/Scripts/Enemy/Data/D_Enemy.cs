using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class D_Enemy : ScriptableObject
{
    public float maxHealth = 100.0f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;
    
    public float closeRangeActionDistance = 1.0f;
    
    public float closeAggroDistance = 3.0f;
    public float farAggroDistance = 4.0f;
    
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.3f;
    public float ledgeCheckDepth = 0.1f;
    
    public LayerMask groundLayerMask;
    public LayerMask wallsLayerMask;
    public LayerMask playerLayerMask;
}

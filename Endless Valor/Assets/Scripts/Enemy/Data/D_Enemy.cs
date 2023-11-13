using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class D_Enemy : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.1f;
    
    public LayerMask groundLayerMask;
    public LayerMask wallsLayerMask;
    public LayerMask playerLayerMask;
}

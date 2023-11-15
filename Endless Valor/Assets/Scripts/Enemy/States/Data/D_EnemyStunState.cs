using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_EnemyStunState : ScriptableObject
{
    public float stunTime = 3.0f;
    public float stunKnockbackTime = 0.2f;
    public float stunKnockbackSpeed = 20f;
    public Vector2 stunKnockbackAngle;
    
}

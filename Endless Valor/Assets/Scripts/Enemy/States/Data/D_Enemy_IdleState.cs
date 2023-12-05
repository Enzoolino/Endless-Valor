using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class D_Enemy_IdleState : ScriptableObject
{
    public float minIdleTime = 1.5f;
    public float maxIdleTime = 3.0f;
}

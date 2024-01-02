using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class D_Enemy_DeadState : ScriptableObject
{
    public float deathDelay = 5.0f;
    public Player.AvailableStats statToGive;
    public float statValueToGive = 1.0f;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
public class D_Enemy_ChargeState : ScriptableObject
{
    public float chargeSpeed = 6.0f;
    public float chargeTime = 3.0f;
}

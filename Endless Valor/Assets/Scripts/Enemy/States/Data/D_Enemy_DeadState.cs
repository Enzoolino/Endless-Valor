using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class D_Enemy_DeadState : ScriptableObject
{
    //public GameObject deathParticle;
    //public GameObject deathBloodParticle;
    public float deathDelay = 5.0f;
}

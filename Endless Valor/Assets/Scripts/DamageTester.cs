using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    private Enemy enemy;
    private AttackDetails attackDetails;


    private void Start()
    {
        enemy = GetComponent<Enemy>();
        attackDetails.damageAmount = 5;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            enemy.TakeDamage(attackDetails);
        }
    }
}

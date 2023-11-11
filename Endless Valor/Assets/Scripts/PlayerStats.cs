using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Serialized
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Animator animator;

    //Privates
    private float currentHealth;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Gracz umar³!");
            //Postaæ umiera
        }
        else
        {
            Debug.Log("Gracz trafiony!");
            animator.SetTrigger("Hit"); 
        }
    }


}

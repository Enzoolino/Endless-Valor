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
    private PlayerMovement playerMovement;

    

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;
        Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Gracz umarł!");
            //Postać umiera
        }
        else
        {
            Debug.Log("Gracz trafiony!");
            playerMovement.isHurt = true;
        }
    }


}

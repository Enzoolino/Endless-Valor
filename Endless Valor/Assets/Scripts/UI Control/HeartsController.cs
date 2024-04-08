using System;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    [SerializeField] private float fullHeartHealth = 20.0f;
    [SerializeField] private float threeQuartersHeartHealth = 15.0f;
    [SerializeField] private float halfHeartHealth = 10.0f;
    [SerializeField] private float oneQuarterHeartHealth = 5.0f;
    [SerializeField] private float emptyHeartHealth = 0.0f;

    //Animator Handling
    private Animator anim;
    
    public static readonly int isFull = Animator.StringToHash("isFull");
    public static readonly int isThreeQuarters = Animator.StringToHash("isThreeQuarters");
    public static readonly int isHalf = Animator.StringToHash("isHalf");
    public static readonly int isQuarter = Animator.StringToHash("isQuarter");
    public static readonly int isEmpty = Animator.StringToHash("isEmpty");
    
    private float playerHealth => Player.Instance.currentHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool(isFull, true);
    }

    private void Update()
    {
        if (Player.Instance.isHurt)
        {
            Debug.Log($"Zmiana serduszek: {playerHealth}");
            if (playerHealth > threeQuartersHeartHealth)
            {
                ResetHeartsFullness();
                anim.SetBool(isFull, true);
            }
            else if (playerHealth <= threeQuartersHeartHealth && playerHealth > halfHeartHealth)
            {
                ResetHeartsFullness();
                anim.SetBool(isThreeQuarters, true);
            }
            else if (playerHealth <= halfHeartHealth && playerHealth > oneQuarterHeartHealth)
            {
                ResetHeartsFullness();
                anim.SetBool(isHalf, true);
            }
            else if (playerHealth <= oneQuarterHeartHealth && playerHealth > emptyHeartHealth)
            {
                ResetHeartsFullness();
                anim.SetBool(isQuarter, true);
            }
            else if (playerHealth <= emptyHeartHealth)
            {
                ResetHeartsFullness();
                anim.SetBool(isEmpty, true);
            }

        }
    }

    private void ResetHeartsFullness()
    {
        anim.SetBool(isFull, false);
        anim.SetBool(isThreeQuarters, false);
        anim.SetBool(isHalf, false);
        anim.SetBool(isQuarter, false);
        anim.SetBool(isEmpty, false);
    }
}

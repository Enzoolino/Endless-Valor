using System;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HeartsController : MonoBehaviour
{
    
    [SerializeField] private float fullHeartHealth = 20.0f;
    [SerializeField] private float threeQuartersHeartHealth = 15.0f;
    [SerializeField] private float halfHeartHealth = 10.0f;
    [SerializeField] private float oneQuarterHeartHealth = 5.0f;
    [SerializeField] private float emptyHeartHealth = 0.0f;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite threeQuartersHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite quarterHeart;
    [SerializeField] private Sprite emptyHeart;
    
    private Image heartImage;
    private float playerHealth => Player.Instance.currentHealth;

    private void Start()
    {
        heartImage = GetComponent<Image>();
        heartImage.sprite = fullHeart;
    }

    private void FixedUpdate()
    {
        if (Player.Instance.isHurt)
        {
            Debug.Log($"Zmiana serduszek: {playerHealth}");
            
            if (playerHealth > threeQuartersHeartHealth)
            {
                heartImage.sprite = fullHeart;
            }
            else if (playerHealth <= threeQuartersHeartHealth && playerHealth > halfHeartHealth)
            {
                heartImage.sprite = threeQuartersHeart;
            }
            else if (playerHealth <= halfHeartHealth && playerHealth > oneQuarterHeartHealth)
            {
                heartImage.sprite = halfHeart;
            }
            else if (playerHealth <= oneQuarterHeartHealth && playerHealth > emptyHeartHealth)
            {
                heartImage.sprite = quarterHeart;
            }
            else if (playerHealth <= emptyHeartHealth)
            {
                heartImage.sprite = emptyHeart;
            }

        }
    }
    
}

using Unity.VisualScripting;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] private GameObject interfaceKeys;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Show info that keys got collected (maybe small UI icon)

        if (other.CompareTag("Player"))
        {
            interfaceKeys.SetActive(true);
            Destroy(gameObject, 0.5f);
            
        }


    }
    
}

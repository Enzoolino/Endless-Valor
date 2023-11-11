using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShowEnemyEmotes : MonoBehaviour
{
    [SerializeField] private GameObject detectionEmote;
    [SerializeField] private GameObject lostTargetEmote;
    [SerializeField] private Transform emotePosition;

    private GroundEnemyAI groundEnemyAI;
    private GameObject instantiatedEmote;
    
    void Start()
    {
        groundEnemyAI = GetComponent<GroundEnemyAI>();
        groundEnemyAI.OnPlayerDetected += GroundEnemyAI_OnPlayerDetected;
        groundEnemyAI.OnPlayerLost += GroundEnemyAI_OnPlayerLost;
    }
    
    private void GroundEnemyAI_OnPlayerLost(object sender, EventArgs e)
    {
        OverwriteEmote();
        SpawnEmote(lostTargetEmote);


        Destroy(instantiatedEmote, groundEnemyAI.timeForSearch);
    }

    private void GroundEnemyAI_OnPlayerDetected(object sender, EventArgs e)
    {
        
        if (instantiatedEmote != null && instantiatedEmote.name == detectionEmote.name + "(Clone)")
        {
            Debug.Log("Emote already instantiated, skipping...");
            return;
        }
        
        // Debug log to check if this block is entered
        Debug.Log("Spawning and destroying emote...");
        
        OverwriteEmote();
        SpawnEmote(detectionEmote);
        Destroy(instantiatedEmote, groundEnemyAI.timeToDetectPlayer);
    }

    private void SpawnEmote(GameObject emote)
    {
        instantiatedEmote = Instantiate(emote, emotePosition.position, emotePosition.rotation);
        instantiatedEmote.transform.parent = gameObject.transform;
    }

    private void OverwriteEmote()
    {
        if (instantiatedEmote != null)
        {
            Destroy(instantiatedEmote);
        }
    }

    
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyEmotesHandler : MonoBehaviour
{
    [SerializeField] private GameObject detectionEmote;
    [SerializeField] private GameObject lostTargetEmote;
    [SerializeField] private Transform emotePosition;
    
    private GameObject instantiatedEmote;
    
    private bool isEmoteVisible;
    
    public void LookForPlayerEmoteHandler()
    {
        OverwriteEmote();
        SpawnEmote(lostTargetEmote);

        if (!isEmoteVisible)
        {
            Destroy(instantiatedEmote);
        }
        
    }

    public void PlayerDetectedEmoteHandler()
    {
        OverwriteEmote();
        SpawnEmote(detectionEmote);

        if (!isEmoteVisible)
        {
            Destroy(instantiatedEmote);
        }
    }
    
    
    public void SetEmoteVisibility(bool isVisible) => isEmoteVisible = isVisible;
    
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

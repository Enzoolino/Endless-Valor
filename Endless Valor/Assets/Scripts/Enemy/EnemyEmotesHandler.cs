using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyEmotesHandler : MonoBehaviour
{
    [SerializeField] private GameObject playerDetectedEmote;
    [SerializeField] private GameObject lookForPlayerEmote;
    [SerializeField] private GameObject stunEmote;
    [SerializeField] private Transform emotePosition;
    
    private GameObject instantiatedEmote;
    
    private bool isEmoteVisible;


    public void HandleEmote(EmoteTypes emoteType)
    {
        OverwriteEmote();

        switch (emoteType)
        {
            case EmoteTypes.PlayerDetectedEmote:
                SpawnEmote(playerDetectedEmote);
                break;
            case EmoteTypes.LookForPlayerEmote:
                SpawnEmote(lookForPlayerEmote);
                break;
            case EmoteTypes.StunEmote:
                SpawnEmote(stunEmote);
                break;
        }
        
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

public enum EmoteTypes
{
    LookForPlayerEmote,
    PlayerDetectedEmote,
    StunEmote
}
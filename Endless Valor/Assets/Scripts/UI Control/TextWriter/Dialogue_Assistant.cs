using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue_Assistant : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageTextPlace;
    [SerializeField] private TextMeshProUGUI speakerTextPlace;
    [SerializeField] private GameObject subtitlesGO;
    [SerializeField] private SubtitleText[] subtitles;

    
    public void StartSubtitles(int subtitlesSequence)
    {
        StartCoroutine(SubtitleCoroutine(subtitlesSequence));
    }

    IEnumerator SubtitleCoroutine(int subtitlesSequence)
    {
        subtitlesGO.SetActive(true);

        foreach (var voiceLine in subtitles.Where(n => n.subtitlesSequence == subtitlesSequence))
        {
            if (TextWriter.CheckIfActiveWriter_Static())
            {
                TextWriter.RemoveWriter_Static(messageTextPlace);
                messageTextPlace.text = "";
            }
            
            TextWriter.AddWriter_Static(messageTextPlace, voiceLine.text, voiceLine.writingSpeed, true);
            speakerTextPlace.text = voiceLine.speaker;

            yield return new WaitForSecondsRealtime(voiceLine.skipTime);
        }
        
        subtitlesGO.SetActive(false);
    }
}

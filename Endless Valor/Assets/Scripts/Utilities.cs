using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public AudioClip RandomizedSound(params AudioClip[] soundClips)
    {
        int randomIndex = UnityEngine.Random.Range(0, soundClips.Length);
        return soundClips[randomIndex - 1];
    }
}

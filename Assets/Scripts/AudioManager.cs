using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponentsInChildren<AudioSource>();
        foreach(AudioSource source in _audioSource)
        {
            source.Play();
        }
    }

   
}

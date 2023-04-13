using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    Renderer[] _volumeBarsRenderers;
    [SerializeField]
    private Material _volumeBarOffMaterial;
    [SerializeField]
    private Material _volumeBarOnMaterial;

    [SerializeField]
    private float _particleVolumeIncrease;
    [SerializeField]
    private float _delayBeforeVolumeDecrease;
    [SerializeField]
    private float _volumeDecreaseStep;

    private float _lastParticleCollisionTime;



    // Update is called once per frame
    private void Update()
    {
        if (Time.time > _lastParticleCollisionTime + _delayBeforeVolumeDecrease)
            _audioSource.volume -= _volumeDecreaseStep * Time.deltaTime;
        float volume = _audioSource.volume;

        for(int i = 0; i< _volumeBarsRenderers.Length; i++)
        {
            float minVolume = (i + 1) * .15f;
            if (volume >= minVolume)
            {
                _volumeBarsRenderers[i].sharedMaterial = _volumeBarOnMaterial;

            }else
            {
                _volumeBarsRenderers[i].sharedMaterial = _volumeBarOffMaterial;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.CompareTag("Particle"))
            {
            if (_audioSource.volume < 1) _audioSource.volume += _particleVolumeIncrease;

            _lastParticleCollisionTime = Time.time;
            }
        }

    }


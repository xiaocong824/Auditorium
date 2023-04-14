using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] _audioSource;

    [SerializeField]
    private float _timeToWIn = 3f;

    private float _sourcesVolumeSum = 0;
    private bool _isVictory;
    private float _victoryTimer;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource source in _audioSource)
        {
            source.Play();
        }
    }

    private void Update()
    {
        /*if (!_isVictory)
        {
            foreach (AudioSource source in _audioSource)
            {
                if (source.volume < 0.8)
                {
                    return;
                    
                }
            }
            _isVictory = true;

        }
        Debug.Log("Win");*/
        if (_isVictory) return;
        _sourcesVolumeSum = 0;

        foreach(AudioSource source in _audioSource)
        {
            _sourcesVolumeSum += source.volume;
            //Debug.Log(_sourcesVolumeSum);
            //Debug.Log("length"+_audioSource.Length);
        }
        if(_sourcesVolumeSum >= _audioSource.Length*0.8 && _victoryTimer < _timeToWIn)
        {
            _victoryTimer += Time.deltaTime;   
        }
        else
        {
            _victoryTimer = 0;
        }
        if(_victoryTimer >= _timeToWIn) 
        {
            _isVictory = true;
            Debug.Log("Win");
        }
    }
}

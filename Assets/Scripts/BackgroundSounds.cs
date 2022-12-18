using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundSounds : MonoBehaviour
{
    public static BackgroundSounds InstanceBackgroundSounds;

    [SerializeField] private AudioClip[] _audioClips;

    private AudioClip _audioClip;
    private AudioSource _audioSource;
    private int _indexAudioClip;

    private void Awake()
    {
        if (InstanceBackgroundSounds != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceBackgroundSounds = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _indexAudioClip = _audioClips.Length - 1;

        StartCoroutine(StartSounds(_audioClips));
    }

    public void OnSliderBackgroundMusicValueChanged(float volume)
    {
        _audioSource.volume = volume;
    }

    private IEnumerator StartSounds(AudioClip[] audioClips)
    {

        while (true)
        {
            _audioClip = audioClips[_indexAudioClip];
            _audioSource.clip = _audioClip;
            _audioSource.Play();

            yield return new WaitForSeconds(_audioClip.length);

            _audioSource.Stop();

            --_indexAudioClip;

            if (_indexAudioClip < 0)
                _indexAudioClip = audioClips.Length - 1;
        }        
    }
}
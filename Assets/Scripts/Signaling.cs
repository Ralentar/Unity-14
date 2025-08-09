using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private Detector _detector;

    private bool _isAlarm;
    private float _volumeStep;
    private float _maxVolume;
    private AudioSource _audioSource;


    private void Awake()
    {
        _isAlarm = false;
        _volumeStep = 0.2f;
        _maxVolume = 1f;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        _detector.OnEntered += HandleEntered;
        _detector.OnExited += HandleExited;
    }

    private void OnDisable()
    {
        _detector.OnEntered -= HandleEntered;
        _detector.OnExited -= HandleExited;
    }

    private void HandleEntered()
    {
        _isAlarm = true;
        StartCoroutine(OnAlarm());
    }

    private void HandleExited()
    {
        _isAlarm = false;
        StartCoroutine(OffAlarm());
    }

    private IEnumerator OnAlarm()
    {
        _audioSource.Play();

        while (_isAlarm)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeStep * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator OffAlarm()
    {
        while (_audioSource.volume > 0 && _isAlarm == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0, _volumeStep * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume == 0 && _isAlarm == false)
            _audioSource.Stop();
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private Detector _detector;

    private float _volumeStep;
    private float _maxVolume;
    private AudioSource _audioSource;


    private void Awake()
    {
        //_alarm = false;
        _volumeStep = 0.5f;
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
        _audioSource.Play();
        StopCoroutine(ChangeVolume(0));
        StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void HandleExited()
    {
        StopCoroutine(ChangeVolume(_maxVolume));
        StartCoroutine(ChangeVolume(0));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        float currentVolume = _audioSource.volume;
        float duration = Mathf.Abs(targetVolume - currentVolume) / _volumeStep;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(currentVolume, targetVolume, elapsedTime / duration);

            yield return null;
        }
    }
}

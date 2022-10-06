using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioBase : MonoBehaviour, IInitializable
{
    private AudioSource _audioSource;

    private bool _isPlaying;

    protected float ClipLength => _audioSource.clip.length;

    public void Initialize() => _audioSource = GetComponent<AudioSource>();

    public virtual void Play()
    {
        if (!_isPlaying)
        {
            _audioSource.Play();
            _isPlaying = true;
        }
    }

    public void Stop()
    {
        if (_isPlaying)
        {
            _audioSource.Stop();
            _isPlaying = false;
        }
    }

    public void Mute() => _audioSource.mute = true;
    public void Unmute() => _audioSource.mute = false;
}
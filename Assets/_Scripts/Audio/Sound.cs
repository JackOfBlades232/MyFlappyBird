using System.Collections;
using UnityEngine;

public class Sound : AudioBase
{
    [SerializeField]
    private SoundType _type;
    
    public SoundType Type => _type;

    private Coroutine _playRoutine;
    
    private IEnumerator PlayOnce()
    {
        base.Play();

        yield return new WaitForSeconds(ClipLength);
        
        Stop();
    }

    public override void Play()
    {
        if (_playRoutine != null)
        {
            Stop();
            StopAllCoroutines();
        }
        
        _playRoutine = StartCoroutine(PlayOnce());
    }
}
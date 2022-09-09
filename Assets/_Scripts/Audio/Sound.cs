using System.Collections;
using UnityEngine;

public class Sound : AudioBase
{
    [SerializeField]
    private SoundType _type;
    
    public SoundType Type => _type;
    
    private IEnumerator PlayOnce()
    {
        base.Play();

        yield return new WaitForSeconds(ClipLength);
        
        Stop();
    }

    public override void Play() => StartCoroutine(PlayOnce());
}
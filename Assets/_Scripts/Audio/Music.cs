using UnityEngine;

public class Music : AudioBase
{
    [SerializeField]
    private MusicType _type;

    public MusicType Type => _type;
}
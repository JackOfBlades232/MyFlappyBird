using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BirdAnimation : MonoBehaviour, IInitializable
{
    private static readonly int _isDisabled =
        Animator.StringToHash("IsDisabled");
    
    private Animator _animator;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(_isDisabled, false);
    }

    public void DisableAnimation()
    {
        _animator.SetBool(_isDisabled, true);
    }
}
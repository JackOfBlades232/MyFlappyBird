using UnityEngine;

[RequireComponent(typeof(BirdJump))]
public class PlayerFacade : MonoBehaviour, IInitializable
{
    private BirdJump _jumper;
    
    public void Initialize()
    {
        _jumper = GetComponent<BirdJump>();
        
        _jumper.Initialize();
    }
}
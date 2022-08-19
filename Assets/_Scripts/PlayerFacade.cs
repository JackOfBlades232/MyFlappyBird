using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdJump))]
public class PlayerFacade : MonoBehaviour, IInitializable
{
    private BirdJump _jumper;

    private GroundStatic _groundStatic;

    public UnityEvent OnDeath;

    public void Initialize()
    {
        _jumper = GetComponent<BirdJump>();
        _groundStatic = FindObjectOfType<GroundStatic>();
        
        _groundStatic.Initialize();
        
        _jumper.Construct(_groundStatic);
        _jumper.Initialize();
        
        _jumper.OnHitGround.AddListener(() => OnDeath?.Invoke());
        
        // TODO : remove this test bit
        // OnDeath.AddListener(() => Debug.Log("Dead"));
    }
}
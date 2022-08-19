using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BirdSpriteSwapper : MonoBehaviour, IInitializable
{
    [SerializeField]
    private Sprite _wingsUpSprite, _wingsDownSprite;

    private GameParams _params;
    
    private SpriteRenderer _renderer;
    
    public void Initialize()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _renderer.sprite = _wingsUpSprite;
    }

    public void Construct(GameParams gameParams) => _params = gameParams;

    public void SwapSprites() => StartCoroutine(TempSwapSprites());

    private IEnumerator TempSwapSprites()
    {
        _renderer.sprite = _wingsDownSprite;

        yield return new WaitForSeconds(_params.BirdWingsDownFrameDuration);

        _renderer.sprite = _wingsUpSprite;
    }
}

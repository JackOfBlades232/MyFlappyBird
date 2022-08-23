using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PipeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerFacade player))
            player.Kill();
    }
}
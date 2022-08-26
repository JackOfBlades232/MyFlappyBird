using UnityEngine;

public class MainUI : MonoBehaviour, IInitializable
{
    public void Initialize() => Deactivate();

    public void Activate() => gameObject.SetActive(true);

    public void Deactivate() => gameObject.SetActive(false);
}
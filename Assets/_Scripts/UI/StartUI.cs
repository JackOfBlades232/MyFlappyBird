using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartUI : MonoBehaviour, IPointerClickHandler, IInitializable
{
    public UnityEvent OnClicked;

    public void Initialize() =>
        OnClicked.AddListener(() => gameObject.SetActive(false));

    public void OnPointerClick(PointerEventData eventData) =>
        OnClicked?.Invoke();
}
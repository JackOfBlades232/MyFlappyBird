using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonBase : MonoBehaviour, IInitializable
{
    protected Button Button;

    public UnityEvent OnClick => Button.onClick;
    
    public virtual void Initialize() => Button = GetComponent<Button>();
}
using UnityEngine;
using UnityEngine.Events;

public class ScriptableEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnable;

    private void OnEnable()
    {
        _onEnable.Invoke();
    }
}

using UnityEngine;
using UnityEngine.Events;

public class UpdateCount : MonoBehaviour
{
    [SerializeField] private string _format;
    [SerializeField] private UnityEvent<string> _onTextChange;

    public void UpdateText(int current, int max)
    {
        _onTextChange.Invoke(string.Format(_format, current, max));
    }
}

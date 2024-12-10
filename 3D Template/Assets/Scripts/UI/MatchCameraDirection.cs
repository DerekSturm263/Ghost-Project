using UnityEngine;

public class MatchCameraDirection : MonoBehaviour
{
    private RectTransform _rt;
    [SerializeField] private Camera _playerCam;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float rotation = _playerCam.transform.rotation.eulerAngles.y;
        _rt.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}

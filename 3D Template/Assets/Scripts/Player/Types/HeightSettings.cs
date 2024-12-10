using UnityEngine;

[System.Serializable]
public struct HeightSettings
{
    [SerializeField] private float _defaultHeight;
    [SerializeField] private float _crouchHeight;

    [SerializeField] private AnimationCurve _headBob;
    [SerializeField] private float _bobFrequency;

    public readonly float Evaluate(bool isCrouching, float movementTime, float speed) => isCrouching ? _crouchHeight : _headBob.Evaluate(Mathf.Repeat(movementTime * _bobFrequency * speed, 1)) * _defaultHeight;
}

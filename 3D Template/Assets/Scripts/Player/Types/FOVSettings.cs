using UnityEngine;

[System.Serializable]
public struct FOVSettings
{
    [SerializeField] private float _defaultFOV;
    [SerializeField] private float _runFOV;
    [SerializeField] private float _crouchFOV;

    [SerializeField] private float _fovChangeSpeed;
    public float FOVChangeSpeed => _fovChangeSpeed;

    public readonly float Evaluate(bool isCrouching, bool isRunning) => isCrouching ? _crouchFOV : isRunning ? _runFOV : _defaultFOV;
}

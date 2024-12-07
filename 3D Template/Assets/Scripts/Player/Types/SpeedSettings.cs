using System;
using UnityEngine;

[Serializable]
public struct SpeedSettings
{
    [SerializeField] private float _crouchMultiplier;
    [SerializeField] private float _walkMultiplier;
    [SerializeField] private float _runMultiplier;

    public readonly float Evaluate(bool isRunning, bool isCrouching) => isCrouching ? _crouchMultiplier : isRunning ? _runMultiplier : _walkMultiplier;
}

using System;
using UnityEngine;

[Serializable]
public struct RunSettings
{
    [SerializeField] private float _runMultiplier;

    public readonly float Evaluate(bool isRunning) => isRunning ? _runMultiplier : 1;
}

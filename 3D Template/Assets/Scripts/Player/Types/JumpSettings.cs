using System;
using UnityEngine;

[Serializable]
public struct JumpSettings
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _multiplier;
    
    public readonly float Evaluate(float time) => _curve.Evaluate(time) * _multiplier;
}

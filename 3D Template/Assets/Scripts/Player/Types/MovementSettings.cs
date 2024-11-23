using System;
using UnityEngine;

[Serializable]
public struct MovementSettings
{
    public enum PhysicsState
    {
        Accelerating,
        Maintaining,
        Decelerating
    }

    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private AnimationCurve _deceleration;
    [SerializeField] private float _decelerationTime;

    [SerializeField] private float _multiplier;
    [SerializeField] private float _topSpeed;

    public readonly float Evaluate(float strength, PhysicsState state, float time) => state switch
    {
        PhysicsState.Accelerating => _acceleration.Evaluate(time),
        PhysicsState.Decelerating => _deceleration.Evaluate(time),
        _ => _topSpeed,
    } * _multiplier * strength;
}

using System;
using UnityEngine;

[Serializable]
public struct TurnSettings
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _sensitivity;

    public readonly Vector2 Evaluate(Vector2 delta, Vector2 currentRot)
    {
        Vector2 newRot = currentRot;

        newRot.y += delta.x * _sensitivity;
        newRot.x += delta.y * _sensitivity;

        return newRot;
    }
}

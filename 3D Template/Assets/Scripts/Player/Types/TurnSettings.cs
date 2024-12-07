using System;
using UnityEngine;

[Serializable]
public struct TurnSettings
{
    [SerializeField] private Range<float> _verticalRange;
    [SerializeField] private float _sensitivity;

    public readonly Vector2 Evaluate(Vector2 delta, Vector2 currentRot)
    {
        Vector2 newRot = currentRot;

        newRot.y += delta.x * _sensitivity;
        newRot.x += delta.y * _sensitivity;

        newRot.x = Mathf.Clamp(newRot.x, _verticalRange.Min, _verticalRange.Max);

        return newRot;
    }
}

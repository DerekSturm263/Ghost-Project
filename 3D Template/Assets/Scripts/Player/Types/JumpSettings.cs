using System;
using UnityEngine;

[Serializable]
public struct JumpSettings
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _endForce;

    public readonly float Evaluate() => _jumpForce;
}

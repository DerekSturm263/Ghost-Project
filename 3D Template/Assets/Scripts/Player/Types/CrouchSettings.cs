using System;
using UnityEngine;

[Serializable]
public struct CrouchSettings
{
    [SerializeField] private float _defaultHeight;
    [SerializeField] private float _crouchHeight;

    public readonly float Evaluate(bool isCrouching) => isCrouching ? _crouchHeight : _defaultHeight;
}

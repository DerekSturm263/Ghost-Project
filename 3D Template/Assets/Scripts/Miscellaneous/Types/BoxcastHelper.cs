using System;
using UnityEngine;

[Serializable]
public struct BoxcastHelper
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _size;
    [SerializeField] private Quaternion _rotation;
    [SerializeField] private LayerMask _layerMask;

    public readonly bool GetHitInfo(Transform transform)
    {
        return Physics.BoxCast(transform.position + _offset, _size, Vector3.zero, _rotation, 0, _layerMask);
    }

    public readonly void Draw(Transform transform)
    {
        Gizmos.DrawCube(transform.position + _offset, _size);
    }
}

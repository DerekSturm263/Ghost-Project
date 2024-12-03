using System;
using UnityEngine;

[Serializable]
public struct BoxcastHelper
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _size;
    [SerializeField] private Quaternion _rotation;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private QueryTriggerInteraction _triggerInteraction;

    public readonly RaycastHit? GetHitInfo(Transform transform)
    {
        if (Physics.BoxCast(transform.position + _offset, _size, Vector3.zero, out RaycastHit hit, _rotation, 0, _layerMask, _triggerInteraction))
            return hit;

        return null;
    }

    public readonly void Draw(Transform transform)
    {
        Gizmos.DrawCube(transform.position + _offset, _size);
    }
}

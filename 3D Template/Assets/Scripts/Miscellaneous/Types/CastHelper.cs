using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct CastHelper
{
    [System.Serializable]
    public struct BoxCastSettings : ICastable
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _size;
        [SerializeField] private Quaternion _rotation;

        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            if (TryGetHitInfo(position, direction, maxDistance, layerMask, triggerInteraction, out RaycastHit hit))
                return hit;

            return null;
        }

        public readonly bool TryGetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, out RaycastHit hit)
        {
            return Physics.BoxCast(position + _offset, _size, direction, out hit, _rotation, maxDistance, layerMask, triggerInteraction);
        }

        public readonly RaycastHit[] GetHitInfoAll(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            return Physics.BoxCastAll(position + _offset, _size, direction, _rotation, maxDistance, layerMask, triggerInteraction);
        }

        public readonly int GetHitInfoNonAlloc(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, RaycastHit[] results)
        {
            return Physics.BoxCastNonAlloc(position + _offset, _size, direction, results, _rotation, maxDistance, layerMask, triggerInteraction);
        }

        public readonly void Draw(Vector3 position)
        {
            Gizmos.DrawCube(position + _offset, _size);
        }
    }

    [System.Serializable]
    public struct SphereCastSettings : ICastable
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius;

        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            if (TryGetHitInfo(position, direction, maxDistance, layerMask, triggerInteraction, out RaycastHit hit))
                return hit;

            return null;
        }

        public readonly bool TryGetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, out RaycastHit hit)
        {
            return Physics.SphereCast(position + _offset, _radius, direction, out hit, maxDistance, layerMask, triggerInteraction);
        }

        public readonly RaycastHit[] GetHitInfoAll(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            return Physics.SphereCastAll(position + _offset, _radius, direction, maxDistance, layerMask, triggerInteraction);
        }

        public readonly int GetHitInfoNonAlloc(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, RaycastHit[] results)
        {
            return Physics.SphereCastNonAlloc(position + _offset, _radius, direction, results, maxDistance, layerMask, triggerInteraction);
        }

        public readonly void Draw(Vector3 position)
        {
            Gizmos.DrawSphere(position + _offset, _radius);
        }
    }

    [System.Serializable]
    public struct CapsuleCastSettings : ICastable
    {
        [SerializeField] private Range<Vector3> _points;
        [SerializeField] private float _radius;

        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            if (TryGetHitInfo(position, direction, maxDistance, layerMask, triggerInteraction, out RaycastHit hit))
                return hit;

            return null;
        }

        public readonly bool TryGetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, out RaycastHit hit)
        {
            return Physics.CapsuleCast(position + _points.Min, position + _points.Max, _radius, direction, out hit, maxDistance, layerMask, triggerInteraction);
        }

        public readonly RaycastHit[] GetHitInfoAll(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            return Physics.CapsuleCastAll(position + _points.Min, position + _points.Max, _radius, direction, maxDistance, layerMask, triggerInteraction);
        }

        public readonly int GetHitInfoNonAlloc(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, RaycastHit[] results)
        {
            return Physics.CapsuleCastNonAlloc(position + _points.Min, position + _points.Max, _radius, direction, results, maxDistance, layerMask, triggerInteraction);
        }

        public readonly void Draw(Vector3 position)
        {

        }
    }

    [System.Serializable]
    public struct RayCastSettings : ICastable
    {
        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            if (TryGetHitInfo(position, direction, maxDistance, layerMask, triggerInteraction, out RaycastHit hit))
                return hit;

            return null;
        }

        public readonly bool TryGetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, out RaycastHit hit)
        {
            return Physics.Raycast(position, direction, out hit, maxDistance, layerMask, triggerInteraction);
        }

        public readonly RaycastHit[] GetHitInfoAll(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            return Physics.RaycastAll(position, direction, maxDistance, layerMask, triggerInteraction);
        }

        public readonly int GetHitInfoNonAlloc(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction, RaycastHit[] results)
        {
            return Physics.RaycastNonAlloc(position, direction, results, maxDistance, layerMask, triggerInteraction);
        }

        public readonly void Draw(Vector3 position)
        {

        }
    }

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private QueryTriggerInteraction _triggerInteraction;

    [SerializeField] private Variant<BoxCastSettings, SphereCastSettings, CapsuleCastSettings, RayCastSettings> _settings;

    public readonly RaycastHit? GetHitInfo(Transform transform)
    {
        return _settings.Get<ICastable>().GetHitInfo(transform.position, _direction, _maxDistance, _layerMask, _triggerInteraction);
    }

    public readonly bool TryGetHitInfo(Transform transform, out RaycastHit hit)
    {
        return _settings.Get<ICastable>().TryGetHitInfo(transform.position, _direction, _maxDistance, _layerMask, _triggerInteraction, out hit);
    }

    public readonly RaycastHit[] GetHitInfoAll(Transform transform)
    {
        return _settings.Get<ICastable>().GetHitInfoAll(transform.position, _direction, _maxDistance, _layerMask, _triggerInteraction);
    }

    public readonly int GetHitInfoNonAlloc(Transform transform, RaycastHit[] results)
    {
        return _settings.Get<ICastable>().GetHitInfoNonAlloc(transform.position, _direction, _maxDistance, _layerMask, _triggerInteraction, results);
    }

    public readonly void Draw(Transform transform)
    {
        _settings.Get<ICastable>().Draw(transform.position);
    }
}

using UnityEngine;

[System.Serializable]
public struct CastHelper
{
    [System.Serializable]
    public struct BoxCastSettings
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _size;
        [SerializeField] private Quaternion _rotation;

        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            if (Physics.BoxCast(position + _offset, _size, direction, out RaycastHit hit, _rotation, maxDistance, layerMask, triggerInteraction))
                return hit;

            return null;
        }

        public readonly void Draw(Vector3 position)
        {
            Gizmos.DrawCube(position + _offset, _size);
        }
    }

    [System.Serializable]
    public struct SphereCastSettings
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius;

        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            if (Physics.SphereCast(position + _offset, _radius, direction, out RaycastHit hit, maxDistance, layerMask, triggerInteraction))
                return hit;

            return null;
        }

        public readonly void Draw(Vector3 position)
        {
            Gizmos.DrawSphere(position + _offset, _radius);
        }
    }

    [System.Serializable]
    public struct CapsuleCastSettings
    {
        [SerializeField] private Range<Vector3> _points;
        [SerializeField] private float _radius;

        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            if (Physics.CapsuleCast(position + _points.Min, position + _points.Max, _radius, direction, out RaycastHit hit, maxDistance, layerMask, triggerInteraction))
                return hit;

            return null;
        }

        public readonly void Draw(Vector3 position)
        {

        }
    }

    [System.Serializable]
    public struct LineCastSettings
    {
        public readonly RaycastHit? GetHitInfo(Vector3 position, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerInteraction)
        {
            return null;
        }

        public readonly void Draw(Vector3 position)
        {

        }
    }

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private QueryTriggerInteraction _triggerInteraction;

    [SerializeField] private Variant<BoxCastSettings, SphereCastSettings, CapsuleCastSettings, LineCastSettings> _settings;

    public readonly RaycastHit? GetHitInfo(Transform transform)
    {
        if (_settings.GetUnderlyingType() == typeof(BoxCastSettings))
            return _settings.Get<BoxCastSettings>().GetHitInfo(transform.position, _direction, _maxDistance, _layerMask, _triggerInteraction);
        else if (_settings.GetUnderlyingType() == typeof(SphereCastSettings))
            return _settings.Get<SphereCastSettings>().GetHitInfo(transform.position, _direction, _maxDistance, _layerMask, _triggerInteraction);
        else
            return _settings.Get<CapsuleCastSettings>().GetHitInfo(transform.position, _direction, _maxDistance, _layerMask, _triggerInteraction);
    }

    public readonly void Draw(Transform transform)
    {
        if (_settings.GetUnderlyingType() == typeof(BoxCastSettings))
            _settings.Get<BoxCastSettings>().Draw(transform.position);
        else if (_settings.GetUnderlyingType() == typeof(SphereCastSettings))
            _settings.Get<SphereCastSettings>().Draw(transform.position);
        else
            _settings.Get<CapsuleCastSettings>().Draw(transform.position);
    }
}

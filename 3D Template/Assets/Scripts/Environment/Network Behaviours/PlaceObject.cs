using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlaceObject : NetworkBehaviour
{
    [SerializeField] private List<GameObject> _objects;
    [SerializeField][Range(0, 1)] private float _chanceToGenerate;
    [SerializeField] private LayerMask _objectPlacement;
    [SerializeField] private Range<Vector3> _positionRange;
    [SerializeField] private Range<Quaternion> _rotationRange;

    [ContextMenu("Place")]
    public void Place() => Place(new());

    public void Place(System.Random random)
    {
        if (random.NextDouble() > 1 - _chanceToGenerate)
            return;

        if (Physics.Linecast(transform.position, transform.position + Vector3.down * 3, out RaycastHit hit, _objectPlacement))
        {
            //Vector3 positionOffset = new(random.Next(_positionRange.Min.x, _positionRange.Max.x), random.Next(), random.Next());

            GameObject obj = _objects[random.Next(0, _objects.Count)];

            Vector3 position = hit.point + new Vector3(0, obj.GetComponent<BoxCollider>().size.y / 2, 0);
            Quaternion rotation = Quaternion.Euler(transform.up);

            GameObject newObject = Instantiate(obj, position, rotation);
            newObject.transform.SetParent(transform, true);

            PlaceObject[] objectPlacers = newObject.GetComponentsInChildren<PlaceObject>(false);
            foreach (PlaceObject obj2 in objectPlacers)
            {
                obj2.Place(random);
            }
        }
    }
}

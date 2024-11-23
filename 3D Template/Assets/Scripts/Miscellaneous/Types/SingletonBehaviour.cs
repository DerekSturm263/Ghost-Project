using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_instance;
}

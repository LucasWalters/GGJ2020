using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliders : MonoBehaviour
{
    [Tooltip("The list of Colliders that should be ignored by this GameObject")]
    public Collider[] CollidersToIgnore;

    void Start()
    {
        foreach (Collider col in CollidersToIgnore)
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), col);
    }

}

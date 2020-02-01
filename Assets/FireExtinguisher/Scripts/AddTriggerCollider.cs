using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class AddTriggerCollider : MonoBehaviour
{
    [Tooltip("The size increase of the original boxcollider")]
    public float sizeIncreaseFactor = 2;

    
    void Awake()
    {
        BoxCollider existingCollider = GetComponent<BoxCollider>();
        BoxCollider newCollider = gameObject.AddComponent<BoxCollider>();
        newCollider.center = existingCollider.center;
        newCollider.size = existingCollider.size * sizeIncreaseFactor;
        newCollider.isTrigger = true;
    }
}

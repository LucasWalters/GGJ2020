using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    public void Cut(GameObject hairObject)
    {
        hairObject.transform.localScale = new Vector3(
               hairObject.transform.localScale.x * 0.9f,
               hairObject.transform.localScale.y * 0.9f,
               hairObject.transform.localScale.z * 0.9f
           );
    }
}

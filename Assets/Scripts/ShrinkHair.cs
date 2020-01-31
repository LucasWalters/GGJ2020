using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkHair : MonoBehaviour
{
    public void Shrink()
    {
        this.transform.localScale = new Vector3(
            transform.localScale.x * 0.9f,
            transform.localScale.y * 0.9f,
            transform.localScale.z * 0.9f
        );
    }
}

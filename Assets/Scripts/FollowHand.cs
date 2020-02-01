using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    [HideInInspector] public Transform handToFollow;
    void Update()
    {
        this.transform.position = handToFollow.position;
    }
}

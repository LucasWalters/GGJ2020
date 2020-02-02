using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DoorAnimations : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject door;

    const float openingDoorDurationInSeconds = 0.5f;
    const float closingDoorDurationInSecond = 0.5f;

    readonly Vector3 doorIsOpened = new Vector3(0, 180, 0);
    readonly Vector3 doorIsClosed = new Vector3(0, 90, 0);


    public void OpenDoor()
    {
        var currentPosition = door.transform.position;

        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }

        door
            .transform
            .DORotate(doorIsOpened, openingDoorDurationInSeconds)
            .SetEase(Ease.InOutQuad);
    }

    public void CloseDoor()
    {
        door
            .transform
            .DORotate(doorIsClosed, closingDoorDurationInSecond)
            .SetEase(Ease.InOutQuad);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OpenDoor();
        }
    }
}

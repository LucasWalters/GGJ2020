using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class NewCustomerHandler : MonoBehaviour
{
    public ChairAnimations chairAnimations;
    public DoorAnimations exitDoorAnimations;
    public DoorAnimations entryDoorAnimations;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            NextCustomer();
        }
    }

    void NextCustomer()
    {
        exitDoorAnimations.OpenDoor();

        chairAnimations
            .MoveChair()
            .OnComplete(() =>
            {
                chairAnimations.ResetChairPosition();
                entryDoorAnimations.OpenDoor();

                chairAnimations
                    .MoveChair()
                    .OnComplete(() =>
                    {
                        entryDoorAnimations.CloseDoor();
                    });

                exitDoorAnimations.CloseDoor();
            });
    }
}

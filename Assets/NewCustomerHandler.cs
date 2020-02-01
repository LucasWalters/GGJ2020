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
    public CustomerFactory customerFactory;
    private GameObject customer;

    public void Start()
    {
        NextCustomer();
    }

    public void NextCustomer()
    {
        exitDoorAnimations.OpenDoor();

        chairAnimations
            .MoveChair()
            .OnComplete(() =>
            {
                if (customer != null)
                {
                    customerFactory.DeleteCustomer(customer);
                }

                chairAnimations.ResetChairPosition();
                customer = customerFactory.CreateCustomer();
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

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
                if (customer != null)
                {
                    customerFactory.DeleteCustomer(customer);
                }

                chairAnimations.ResetChairPosition();
                customer = customerFactory.CreateCustomer();
                customer.transform.localPosition = new Vector3(0, 0, 2);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireExtinguisherHandleRotator : OVRGrabbable {
    [Header("FireExtinguisherHandleRotator Settings")]

    [Tooltip("The fire extinguisher controller that handles the foam effect.")]
    public FireExtinguisherController fireExtinguisherController;


    //public override void OnValueChanged(ControllableEventArgs e)
    //{
    //    base.OnValueChanged(e);
    //    fireExtinguisherController.SetSpouting(IsGrabbed() && e.value < 0.9);
    //}
}

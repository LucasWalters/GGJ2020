//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using VRTK;

//[RequireComponent(typeof(VRTK_SnapDropZone))]
//public class DisableCollidersOnSnapped : MonoBehaviour {
    
//	void Awake () {
//        GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += ObjectSnapped;
//	}


//    private void ObjectSnapped(object sender, SnapDropZoneEventArgs e)
//    {
//        foreach (Collider col in e.snappedObject.GetComponentsInChildren<Collider>()) {
//            col.enabled = false;
//        }
//    }
//}

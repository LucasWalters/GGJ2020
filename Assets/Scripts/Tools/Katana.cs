using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : BaseTool
{

    public override void ExecuteAction(GameObject hairObject)
    {
        hairObject.GetComponent<Rigidbody>().useGravity = true;
    }
}

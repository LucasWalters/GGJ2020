using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trimmer : BaseTool
{
    public override void ExecuteAction(GameObject hairObject)
    {
        hairObject.SetActive(false);
    }
}

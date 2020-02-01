using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveChair : MonoBehaviour
{

    public GameObject chair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            chair.transform.DOMove(chair.transform.position + (Vector3.left * 4), 2).SetEase(Ease.InOutQuad).OnComplete(()=>GetNewChair());
        }
    }

    void GetNewChair() {
        Destroy(chair.gameObject);
        // Create new one at Vector3.right * 4 ofzo
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewCustomerHandler : MonoBehaviour
{
    const float Invisible = 0;
    const float Visible = 100;
    const float DurationAnimationInSeconds = 0.5f;

    private Vector3 MoveInterval = (Vector3.left * 5);
    private Vector3 StartPosition = new Vector3(0, 0, 0) - (Vector3.left * 5);

    public GameObject currentChair;


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
        MoveChair(currentChair)
            .OnComplete(() =>
            {
                ResetChairPosition();
                MoveChair(currentChair);
            });
    }

    void ResetChairPosition()
    {
        currentChair
            .transform
            .position = StartPosition;
    }

    DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> MoveChair(GameObject chair)
    {
        var currentPosition = currentChair.transform.position;

        return chair
            .transform
            .DOMove(currentPosition + MoveInterval, DurationAnimationInSeconds)
            .SetEase(Ease.InOutQuad);
    }
}

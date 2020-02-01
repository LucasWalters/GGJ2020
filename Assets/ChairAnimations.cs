using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ChairAnimations : MonoBehaviour
{
    const float DurationAnimationInSeconds = 0.5f;
    private Vector3 MoveInterval = (Vector3.left * 8);
    private Vector3 StartPosition = new Vector3(0, 0, 0) - (Vector3.left * 8);


    public void ResetChairPosition()
    {
        this.transform.position = StartPosition;
    }
    public TweenerCore<Vector3, Vector3, VectorOptions> MoveChair()
    {
        var currentPosition = this.transform.position;

        return this.transform.DOMove(currentPosition + MoveInterval, DurationAnimationInSeconds).SetEase(Ease.InOutQuad);
    }
}

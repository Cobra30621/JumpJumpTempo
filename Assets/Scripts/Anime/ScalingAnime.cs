using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // 添加這個DOTween所在的名字空間

public class ScalingAnime : MonoBehaviour
{
    public float scalingSize;
    public float scalingDur;
    public Vector3 startVec;

    void Awake()
    {
        startVec = transform.localScale;
    }

    [ContextMenu("播放動畫")]
    public void Scaling(){
        Reset();

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(startVec * scalingSize, scalingDur))
        .Append(transform.DOScale(startVec, scalingDur));

    }

    public void Reset(){
        transform.localScale = startVec;
    }
}

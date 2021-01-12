using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // 添加這個DOTween所在的名字空間


public class TextInfoAnime : MonoBehaviour
{

    public float scalingSize;
    public float scalingDur;
    public float waitDur;
    public float fadingDur;
    [SerializeField] private Vector3 startVec;
    [SerializeField] private Text text;
    public Ease scaleEase;

    void Awake()
    {
        startVec = transform.localScale;
    }

    public void PlayAnime(string info){
        text.text = info;
        PlayAnime();
    }

    [ContextMenu("播放動畫")]
    public void PlayAnime(){
        Reset();
        Vector3 vec = new Vector3(startVec.x, startVec.y, 0);
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.DOScale(vec * scalingSize, scalingDur).SetEase(scaleEase))
        .AppendInterval(waitDur)
        .Append(text.DOFade(0f , fadingDur));
    }

    public void Reset(){
        transform.localScale = startVec;
        text.DOFade(1f , 0f);
    }
}

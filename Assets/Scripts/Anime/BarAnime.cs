using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // 添加這個DOTween所在的名字空間

public class BarAnime : IBarAnime
{
    private Sequence ProgressBarSequence; 
    [SerializeField]private Image img_bar;
    private bool CanPlayingAnime;

    public float dur = 0.5f;
    public override void PlayAnime (float start, float end){
        img_bar.fillAmount = start;
        ProgressBarSequence.OnStart(OnStart)
                .Append(img_bar.gameObject.transform.DOScaleY(end, dur))
                .OnComplete((OnComplete)); 
    }



    private void OnStart(){
        CanPlayingAnime = true;
    }

    private void OnComplete(){
        CanPlayingAnime = false;
    }

}


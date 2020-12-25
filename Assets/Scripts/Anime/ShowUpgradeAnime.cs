using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // 添加這個DOTween所在的名字空間

public class ShowUpgradeAnime : MonoBehaviour
{
    public float scalingSize;
    public float scalingDur;
    public float fadingDur;
    public Vector3 startVec;
    private Text text;
    // Start is called before the first frame update
    void Awake()
    {
        startVec = transform.localScale;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("播放動畫")]
    public void Scaling(){
        Reset();

        Vector3 vec = new Vector3(startVec.x, startVec.y, 0);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(vec * scalingSize, scalingDur))
        .Append(text.DOFade(0f , fadingDur));

        
    }

    public void Reset(){
        transform.localScale = startVec;
        text.DOFade(0.8f , 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // 添加這個DOTween所在的名字空間

public class Question : MonoBehaviour
{
    public float scalingSize;
    public float scalingDur;
    public Vector3 startVec;

    [SerializeField] private Text lab_outcome;

    private float questionInterval;
    private MainGameUI _mainGameUI;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestion(float questionInterval){
        this.questionInterval = questionInterval;
        Scaling();
    }

    [ContextMenu("播放動畫")]
    public void Scaling(){

        this.StartCoroutine(NextQuestion());
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(startVec * scalingSize, scalingDur))
        .Append(transform.DOScale(startVec, scalingDur));

    }

    IEnumerator NextQuestion(){
        yield return new WaitForSeconds(questionInterval);
        Debug.Log($"等待完{questionInterval}秒後，創造下一題");
        _mainGameUI.CreateQuestion();
    }

    public void Hide(){
        lab_outcome.gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }

    public void ShowOutcome(bool outcome){
        lab_outcome.gameObject.SetActive(true);
        if(outcome){
            lab_outcome.text = "O";
        }
        else{
            lab_outcome.text = "X";
        }
    }

    public void SetMainGameUI(MainGameUI mainGameUI){
        _mainGameUI = mainGameUI;
    }
}

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

    private Text lab_question;
    private float questionInterval;
    private MainGameUI _mainGameUI;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        lab_question = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestion(float questionInterval){
        this.questionInterval = questionInterval;
        this.StartCoroutine(NextQuestion());
        Scaling();
    }

    

    IEnumerator NextQuestion(){
        lab_question.color = Color.yellow;
        yield return new WaitForSeconds(questionInterval);
        Debug.Log($"等待完{questionInterval}秒後，創造下一題");
        _mainGameUI.CreateQuestion();
    }

    public void Hide(){
        lab_outcome.gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }

    public void ShowOutcome(bool outcome){
        // Scaling();
        // lab_outcome.gameObject.SetActive(true);
        if(outcome){
            lab_outcome.text = "O";
            lab_question.color = Color.green;
        }
        else{
            lab_outcome.text = "X";
            lab_question.color = Color.red;
        }
    }

    [ContextMenu("播放動畫")]
    public void Scaling(){
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(startVec * scalingSize, scalingDur))
        .Append(transform.DOScale(startVec, scalingDur));

    }

    public void SetMainGameUI(MainGameUI mainGameUI){
        _mainGameUI = mainGameUI;
    }
}

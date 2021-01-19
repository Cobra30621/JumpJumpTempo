using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private Text lab_rank;
    [SerializeField] private Text lab_score;
    [SerializeField] private Text lab_name;

    // Start is called before the first frame update
    public void SetInfo(string rank, string score,string name ){
        lab_score.text = score;
        lab_rank.text = rank;
        lab_name.text = name;
    }

    public void PlayShowAnime(){
        OnStart();
    }

    public void OnStart(){
        transform.localScale = Vector3.one;
    }

}

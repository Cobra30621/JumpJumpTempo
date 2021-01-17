using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreCell : MonoBehaviour
{
    public Text lab_name;
    public Text lab_score;
    
    public void SetModel(UserData data){
        lab_name.text = data.userName;
        lab_score.text = data.score + "";
    }
}

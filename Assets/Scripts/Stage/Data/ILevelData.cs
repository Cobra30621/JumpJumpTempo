using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ILevelData {
    public int needCorrectCount = 3;
    public float addTime = 3;

    [HideInInspector]
    public float subTime = 3;
    
    [HideInInspector]
    public Color barColor;
    // public Sprite img_bar;

    [HideInInspector]
    public string[] QAs;

    

    [HideInInspector]
    public string[] QBs;
    

    public virtual void SetQuetion(){}

    public virtual void Init(){
        needCorrectCount = 3;
        addTime = 3;
        subTime = 3;
        barColor = Color.white;
    }

    public string GetQuetion(int r){
        int r2;
		if(r == 0){
            if(QAs.Length == 0){
                Debug.Log($"QAs沒有題目，請賦予題目");
                return "";
            }

			r2 = Random.Range(0,QAs.Length);
			return QAs[r2];
		}
		else{
            if(QBs.Length == 0){
                Debug.Log($"QBs沒有題目，請賦予題目");
                return "";
            }

			r2 = Random.Range(0,QBs.Length);
			return QBs[r2];
		}
    }


}
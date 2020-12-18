using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// [CreateAssetMenu(fileName= "StickerData", menuName= "Pratiti/Craete StickerData")]
public class ILevelData : ScriptableObject{
    public string levelName;
    public int needCorrectCount;
    public float addTime;
    public float subTime;
    
    public Color barColor;
    public Sprite img_bar;

    public string QA_name;
    public string[] QAs;
    
    public string QB_name;
    public string[] QBs;
    

    public string GetQuetion(int r){
        int r2;
		if(r == 0){
            if(QAs == null){
                Debug.Log($"{levelName}的QAs沒有題目，請賦予題目");
                return "";
            }

			r2 = Random.Range(0,QAs.Length);
			return QAs[r2];
		}
		else{
            if(QBs == null){
                Debug.Log($"{levelName}的QBs沒有題目，請賦予題目");
                return "";
            }

			r2 = Random.Range(0,QBs.Length);
			return QBs[r2];
		}
    }


}
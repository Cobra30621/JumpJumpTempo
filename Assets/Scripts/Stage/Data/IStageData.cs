using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IStageData : ScriptableObject{
    [Header("輸入完資料，記得要去設定資料")]
    public Color[] Colors = {Color.red, new Color(1f, 0.64f, 0), Color.yellow, Color.green,
                            new Color(0, 1f, 1f), Color.blue, new Color(0.64f, 0, 1f), Color.white, Color.black }; 
    public string[] upgradeTexts = {"OK", "Good", "Great", "Prefect", "Excellent", 
                            "Pro", "Master", "God", "C8763"};

    protected int[] needCorrectedCounts = {3,5,7,10,13,15,15,15,20};
    protected int[] addTimes = {1,1,1,2,2,2,3,5,5};

    public string stageName;
    public string questionName;
    public float totalTime;
    public float subTime = 3f;
    public string QA_name;
    public string QB_name;
    

    public virtual void Init(){
        // needCorrectedCounts = new int[];
        // needCorrectedCounts = {3,5,7,10,13,15,15,15,20};
        // addTimes =  new int[];
        // addTimes = {1,1,1,2,2,2,3,5,5};
        totalTime = 20f;
        subTime = 3f;
    }

    public virtual void SetQuetion(){}

    public virtual ILevelData[] GetLevelDatas(){
        return null;
    }

}
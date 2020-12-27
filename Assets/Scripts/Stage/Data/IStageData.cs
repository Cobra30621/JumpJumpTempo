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
    public string stageName;
    public string questionName;
    public float totalTime;
    public float subTime = 1;
    public string QA_name;
    public string QB_name;
    

    [ContextMenu("初始化關卡資料")]
    public virtual void Init(){
        // foreach (var data in levelDatas)
        // {
        //     data.Init();
        // }

        SetQuetion();
    }

    public virtual void SetQuetion(){}

    public virtual ILevelData[] GetLevelDatas(){
        return null;
    }

}
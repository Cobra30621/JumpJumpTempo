using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class IStageData : ScriptableObject{
    [Header("輸入完資料，記得要去設定資料")]
    public Color[] Colors = {Color.red, new Color(1f, 0.64f, 0), Color.yellow, Color.green,
                            new Color(0, 1f, 1f), Color.blue, new Color(0.64f, 0, 1f), Color.white, Color.gray, Color.black }; 
    public string stageName;
    public string questionName;
    public float totalTime;
    public string QA_name;
    public string QB_name;
    public ILevelData [] levelDatas;

    [ContextMenu("初始化關卡資料")]
    public virtual void Init(){
        // foreach (var data in levelDatas)
        // {
        //     data.Init();
        // }

        SetQuetion();
    }

    [ContextMenu("輸入完資料後，設置關卡資料")]
    public virtual void SetQuetion(){
        foreach (var data in levelDatas)
        {
            data.SetQuetion();
        }

        for (int i = 0; i < levelDatas.Length; i++)
        {
            levelDatas[i].Init();
            if(i < Colors.Length)
                levelDatas[i].barColor = Colors[i];
            else
                levelDatas[i].barColor = Colors[7];
        } 
    }

    public virtual ILevelData[] GetLevelDatas(){
        return levelDatas;
    }

}
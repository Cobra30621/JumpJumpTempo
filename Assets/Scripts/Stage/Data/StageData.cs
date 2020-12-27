
using UnityEngine;

[CreateAssetMenu(fileName= "StageData", menuName= "Stage/Craete StageData")]
public class StageData : IStageData{
    public LevelData [] levelDatas;

    [ContextMenu("輸入完資料後，設置關卡資料")]
    public override void SetQuetion(){
        foreach (var data in levelDatas)
        {
            data.SetQuetion();
        }

        for (int i = 0; i < levelDatas.Length; i++)
        {
            // levelDatas[i].Init();
            if(i < Colors.Length)
                levelDatas[i].barColor = Colors[i];
            else
                levelDatas[i].barColor = Colors[levelDatas.Length -1];
        } 
    }

    public override ILevelData[] GetLevelDatas(){
        return levelDatas;
    }
}
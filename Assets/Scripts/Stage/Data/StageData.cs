
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
            // 設置顏色
            if(i < Colors.Length)
                levelDatas[i].barColor = Colors[i];
            else
                levelDatas[i].barColor = Colors[Colors.Length -1];

            // 設置通關所需題數
            if(i < needCorrectedCounts.Length)
                levelDatas[i].needCorrectCount = needCorrectedCounts[i];
            else
                levelDatas[i].needCorrectCount = needCorrectedCounts[needCorrectedCounts.Length - 1];

            // 設置通關加的時間
            if(i<addTimes.Length)
                levelDatas[i].addTime = addTimes[i];
            else
                levelDatas[i].addTime = addTimes[addTimes.Length - 1];
        } 
    }

    public override ILevelData[] GetLevelDatas(){
        return levelDatas;
    }
}
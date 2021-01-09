
using UnityEngine;

[CreateAssetMenu(fileName= "StageData", menuName= "Stage/Craete MathStageData")]
public class MathStageData : IStageData{

    public int prime;
    public MathLevelData [] mathLevelDatas;

    

    [ContextMenu("初始設置關卡資料(數學)")]
    public override void Init(){
        base.Init();
        foreach (var data in mathLevelDatas)
        {
            data.Init(prime);
        }

        for (int i = 0; i < mathLevelDatas.Length; i++)
        {
            Debug.Log($"設置：{i}");
            // 設置顏色
            if(i < Colors.Length)
                mathLevelDatas[i].barColor = Colors[i];
            else
                mathLevelDatas[i].barColor = Colors[mathLevelDatas.Length -1];
            
            // 設置通關所需題數
            if(i < needCorrectedCounts.Length)
                mathLevelDatas[i].needCorrectCount = needCorrectedCounts[i];
            else
                mathLevelDatas[i].needCorrectCount = needCorrectedCounts[needCorrectedCounts.Length - 1];
            Debug.Log($"needCorrectedCounts[i]：{needCorrectedCounts[i]}");
            Debug.Log($"mathLevelDatas[i].needCorrectCount：{mathLevelDatas[i].needCorrectCount}");

            // 設置通關加的時間
            if(i<addTimes.Length)
                mathLevelDatas[i].addTime = addTimes[i];
            else
                mathLevelDatas[i].addTime = addTimes[addTimes.Length - 1];
            } 
    }

    [ContextMenu("輸入完資料後，設置關卡資料(數學)")]
    public override void SetQuetion(){
        foreach (var data in mathLevelDatas)
        {
            data.SetQuetion(prime);
        }

    }

    public override ILevelData[] GetLevelDatas()
    {
        return mathLevelDatas;
    }



    

}
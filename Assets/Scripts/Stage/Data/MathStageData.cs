
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
    }

    [ContextMenu("輸入完資料後，設置關卡資料(數學)")]
    public override void SetQuetion(){
        foreach (var data in mathLevelDatas)
        {
            data.SetQuetion(prime);
        }

        for (int i = 0; i < mathLevelDatas.Length; i++)
        {
            // mathLevelDatas[i].Init();
            if(i < Colors.Length)
                mathLevelDatas[i].barColor = Colors[i];
            else
                mathLevelDatas[i].barColor = Colors[7];
        } 
    }

    public override ILevelData[] GetLevelDatas()
    {
        return mathLevelDatas;
    }



    

}
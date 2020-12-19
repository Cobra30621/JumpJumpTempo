using UnityEngine;

[System.Serializable]
public class LevelData : ILevelData{
    public string QuetionA;
    public string QuetionB;

    public override void SetQuetion(){
        QAs = QuetionA.Split(' ');
        QBs = QuetionB.Split(' ');
    }

}
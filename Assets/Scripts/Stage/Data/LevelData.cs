using UnityEngine;

[System.Serializable]
public class LevelData : ILevelData{
    public string QuestionA;
    public string QuestionB;

    public override void SetQuetion(){
        QAs = QuestionA.Split(' ');
        QBs = QuestionB.Split(' ');
    }

}
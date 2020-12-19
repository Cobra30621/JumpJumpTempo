using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MathLevelData : ILevelData{
    private int prime;
    public int min;
    public int max;

    private List<string> ListQAs = new List<string>();
    private List<string> ListQBs = new List<string>();

    public void Init(int prime)
    {
        base.Init();
        this.prime = prime;
        
    }

    public void SetQuetion(int prime){
        this.prime = prime;
        for (int i = min; i < max; i++){
            if (i % prime == 0)
                ListQAs.Add(i + "");
            else 
                ListQBs.Add(i + "");
        }

        QAs = new string[ListQAs.Count];
        for (int a = 0; a < ListQAs.Count ; a++){
            Debug.Log("設置問題A" + ListQAs[a]);
            QAs[a] = ListQAs[a];
        }

        QBs = new string[ListQBs.Count];
        for (int b = 0; b < ListQBs.Count ; b++){
            Debug.Log("設置問題B" + ListQBs[b]);
            QBs[b] = ListQBs[b];
        }
    }

    
}
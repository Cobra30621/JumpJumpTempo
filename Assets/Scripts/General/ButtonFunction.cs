using UnityEngine;
using System;
using System.Collections;

public class ButtonFunction : MonoBehaviour
{
    // 將資料加入排行榜 ，會放在結束時出現的按鈕那邊
	public void AddScoreToLeaderBoard(){
		GameMediator.Instance.AddScoreToLeaderBoard();
	} 

    // 暫停遊戲
    public void Pause(){
        GameMediator.Instance.Pause();
    }

    // 停止暫停遊戲
    public void EndPause(){
        GameMediator.Instance.EndPause();
    }

    // 清除存檔
    public void DeleteAll(){
        GameMediator.Instance.DeleteAll();
    }
}

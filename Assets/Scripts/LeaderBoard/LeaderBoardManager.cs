using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.Events;

public class UserData{
    public string userName;
    public int score;
    public UserData(string userName, int score){
        this.userName = userName;
        this.score = score;
    }
}


public class LeaderBoardManager : MonoBehaviour
{
    private const string url = "http://dreamlo.com/lb/";
	
	private const string privateCode = "_d3gT2uRP0-zwAO9T0MUoAD807gDj260CbQsNUV3ni7w";
	private const string publicCode = "5ffe9ae8778d3c92a00281dc";


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateNewHighScore("Dio", 1500));
        // StartCoroutine(GetScoreData());
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator CreateNewHighScore(string playerName, int score)
	{
        Debug.Log(url + privateCode + "/add/" + UnityWebRequest.EscapeURL(playerName) + "/" + score);
		UnityWebRequest request = new UnityWebRequest(url + privateCode + "/add/" + UnityWebRequest.EscapeURL(playerName) + "/" + score);
        yield return request.SendWebRequest();
        if(request.isHttpError|| request.isNetworkError){
            Debug.LogError(request.error); 
            
        }
        else{
            Debug.Log("新增完成");
        }
	}

    public static IEnumerator GetScoreData(UnityAction<List<UserData>> callBack)
	{
		UnityWebRequest request = UnityWebRequest.Get(url + publicCode + "/json");
        yield return request.SendWebRequest();
        if(request.isHttpError|| request.isNetworkError){
            Debug.LogError(request.error); 
            
        }
        else{
            Debug.Log(request.downloadHandler.text);
            var data = JsonMapper.ToObject(request.downloadHandler.text);
            var userDatas =  data["dreamlo"]["leaderboard"]["entry"];
            List<UserData> userDataList = new List<UserData>();

            if(userDatas.IsArray){
                foreach (JsonData user in userDatas)
                {
                    Debug.Log(user["name"]);
                    userDataList.Add( new UserData(user["name"].ToString(), int.Parse(user["score"].ToString())));
                }
            }
            else{
                Debug.Log(userDatas["name"]);
                userDataList.Add( new UserData(userDatas["name"].ToString(), int.Parse(userDatas["score"].ToString())));
            }
            callBack(userDataList);
        }
	}
}

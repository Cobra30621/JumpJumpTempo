using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    private string url = "https://script.google.com/macros/s/AKfycby8paaFVRQzx804FeOgy7d6BDfrQC1Er0tDLD9LElJuiJYH7_pRKq4T/exec";
    private string method;
    private string id;
    private string json;


    void Awake()
    {
        SaveData("3倍數", "Json");
        LoadData("2的倍數");
        
    }

    public void SaveData(string id, string json){
        this.method = "write";
        this.id = id;
        this.json = json;
        StartCoroutine(Upload());
    }

    public void LoadData(string id){
        this.method = "read";
        this.id = id;
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", method);
        form.AddField("id", id);
        form.AddField("json", json);

        using (UnityWebRequest www = UnityWebRequest.Post(url , form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                print(www.downloadHandler.text);
                Debug.Log("Form upload complete!");
                if(method == "read"){
                    json = www.downloadHandler.text;
                    OnLoadComplete();
                }
                    
                
            }
        }
    }

    public void OnLoadComplete(){
        Debug.Log($"Load:{json}");
    }
}

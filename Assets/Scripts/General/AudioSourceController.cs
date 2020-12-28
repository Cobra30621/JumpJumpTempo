using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController
{

    //此指令碼不需要繼承MonoBehaviour   

    
    public static Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();


    /// <summary>
    /// 需要播放某個音效的時候需要呼叫此方法就可以了
    /// </summary>
    /// <param name="dir">這是你音效的路徑, 必須在Resources目錄下</param>
    /// <param name="name">音效的名稱</param>
    public static void PlaySnd(string name)
    {
        string dir = "audio" ;
        AudioClip clip = LoadClip(dir, name);
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, new Vector3(0,0,-10));   //Vector3.zero 是播放音樂的位置(0,0,0)
        else                                                   // 如果主攝像機離這個位置遠的話會出現聲音小或者聽不見的情況
            Debug.LogError("Clip is Missing" + name);
    }
    public static AudioClip LoadClip(string dir, string name)
    {
        if (!audioDic.ContainsKey(name))
        {
            string dirMusic = dir + "/" + name;
            AudioClip clip = Resources.Load(dirMusic) as AudioClip;
            if (clip != null)
                audioDic.Add(clip.name, clip);
        }
        return audioDic[name];
    }
}
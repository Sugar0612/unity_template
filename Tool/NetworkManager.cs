/*
author: ShiyiTang
time: 2024/07/05-16:23:08
describe: 异步文件读取
*/

using Cysharp.Threading.Tasks;
using sugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager _Instance;

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
      
    }   

    /// <summary>
    /// 根据 path_url 找到文本内容
    /// </summary>
    /// <param name="path_url"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async UniTask DownLoadTextFromServer(string path_url, Action<string> callback = null)
    {
        UnityWebRequest req = UnityWebRequest.Get(path_url);
        await req.SendWebRequest();

        string content = req.downloadHandler.text;

        if (callback != null)
        {
            callback(content);
        }
    }  
}

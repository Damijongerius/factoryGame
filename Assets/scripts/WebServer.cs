using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.Networking;

public class WebServer
{

   public IEnumerator sendSaveFile(string data)
    {
        Debug.Log("running court");
        WWWForm form = new WWWForm();
        form.AddField("sendJson", data);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/sendThis", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                www.Dispose();
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                www.Dispose();

                // vanuit c# json file naar server sturen
                
            }
        }
    }
}

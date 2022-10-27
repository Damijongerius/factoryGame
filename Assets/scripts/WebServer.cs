using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebServer
{
    public IEnumerator DoNoting()
    {
        Debug.Log("het ligt aan de functie");
        return null;
    }
   public IEnumerator sendSaveFile(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("sendJson", data);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/recieve", form))
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
            }
        }
    }

    public IEnumerator CreateUser(Guid GUID, string UserName, string password, Func<ReturnedData, bool> retrn)
    {
        WWWForm form = new WWWForm();
        form.AddField("GUID", GUID.ToString());
        form.AddField("UserName", UserName);
        form.AddField("Password", password);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/CreateUser", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                ReturnedData RD = new ReturnedData();

                RD = JsonConvert.DeserializeObject<ReturnedData>(www.downloadHandler.text);
                retrn(RD);
            }
            www.Dispose();
        }
    }

    public IEnumerator loadUser(string UserName, string password, Func<ReturnedData,bool> retrn)
    {
        WWWForm form = new WWWForm();
        form.AddField("UserName", UserName);
        form.AddField("Password", password);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/LoadUser", form))
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

                ReturnedData RD = new ReturnedData();

                RD = JsonConvert.DeserializeObject<ReturnedData>(www.downloadHandler.text);
                retrn(RD);
                www.Dispose();
            }
        }
    }
}

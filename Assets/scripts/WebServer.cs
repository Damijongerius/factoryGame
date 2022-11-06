using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class WebServer
{
    public IEnumerator sendSaveFile(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("sendJson", data);
        form.AddField("GUID", User.GetInstance().guid.ToString());
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/Save/savefile", form))
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

    public IEnumerator getSaveFile(int id, Func<string, bool> retrn)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", id);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/Load/savefile", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error.ToString());
            }
            else
            {
                retrn(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator getSaveFiles(int[] id, Func<string, bool> retrn)
    {
        WWWForm form = new WWWForm();
        for (int i = 0; i < id.Length; i++)
        {
            form.AddField("ID", id[i]);
        }
        form.AddField("GUID", User.GetInstance().guid.ToString());
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/Load/savefile", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error.ToString());
            }
            else
            {
                retrn(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetProfiles(Func<string, bool> retrn)
    {
        WWWForm form = new WWWForm();
        form.AddField("GUID", User.GetInstance().guid.ToString());
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/Load/profiles", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error.ToString());
            }
            else
            {
                retrn(www.downloadHandler.text);

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

                ReturnedData RD = new ReturnedData();

                RD = JsonConvert.DeserializeObject<ReturnedData>(www.downloadHandler.text);
                retrn(RD);
                www.Dispose();
            }
        }
    }

    public IEnumerator loadUser(string GUID,string UserName, string password, Func<ReturnedData, bool> retrn)
    {
        WWWForm form = new WWWForm();
        form.AddField("GUID", GUID);
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

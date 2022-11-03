using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

    public IEnumerator GetSaveFiles()
    {
        string profiles = GetProfilesFromGUID();

        if(profiles != null)
        {
            Profile[] pf = JsonConvert.DeserializeObject<Profile[]>(profiles);
            Debug.Log(pf[0]);

        }
        else
        {
            return null;
        }

        return null;

        string GetProfilesFromGUID()
        {
            WWWForm form = new WWWForm();
            form.AddField("GUID", User.GetInstance().guid.ToString());
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/Load/profiles", form))
            {
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    return www.error.ToString();
                }
                else
                {
                    return www.downloadHandler.text;

                }
            }
        }

        string getSaveFile(int id)
        {
            WWWForm form = new WWWForm();
            form.AddField("ID", id);
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/Load/savefile", form))
            {
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    return www.error.ToString();
                }
                else
                {
                    return www.downloadHandler.text;

                }
            }
        }

        string getSaveFiles(List<int> id)
        {
            WWWForm form = new WWWForm();
            string idS = "[";
            foreach (int idItem in id)
            {
                idS = idS + idItem;

                if (idItem != id[id.Count])
                {
                    idS = idS + ",";
                }
                else
                {
                    idS = idS + "]";
                }
            }
            form.AddField("ID", idS);
            form.AddField("GUID", User.GetInstance().guid.ToString());
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/Load/savefile", form))
            {
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    return www.error.ToString();
                }
                else
                {
                    return www.downloadHandler.text;
                }
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

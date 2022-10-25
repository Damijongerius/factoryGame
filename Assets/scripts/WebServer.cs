using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
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
        Debug.Log("running court");
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
        Debug.Log("done");
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
                
                retrn(new ReturnedData("Error Connection Failed!!", ReturnedData.Returning.ConnectionError));

            }
            else
            {
                if(www.downloadHandler.text == "User Created")
                {
                    retrn(new ReturnedData("User has been created, Continue to login", ReturnedData.Returning.Success));
                }
                else if(www.downloadHandler.text == "AlreadyExists")
                {
                    retrn(new ReturnedData("Sorry This User Already Exists!!", ReturnedData.Returning.AlreadyExists));
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    retrn(new ReturnedData("Could not Resolve Problem Error: " + www.result, ReturnedData.Returning.DefaultError));
                }

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
            Debug.Log(www.downloadHandler.text);

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
}

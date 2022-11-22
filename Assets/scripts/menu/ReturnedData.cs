using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnedData
{
    public int status;
    public string Message;
    public Info info;

    public class Info
    {
        public string UserName;
        public string GUID;
    }
}

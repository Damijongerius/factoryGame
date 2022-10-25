using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnedData
{
    private readonly string info;

    private readonly Returning returnedState;

    public ReturnedData(string _info, Returning _returnedState)
    {
        this.info = _info;
        this.returnedState = _returnedState;
    }

    public enum Returning
    {
        Json, AlreadyExists, DefaultError, ConnectionError, Success
    }

    public string GetInfo()
    {
        return info;  
    }

    public Returning GetReturning()
    {
        return returnedState;
    }
}

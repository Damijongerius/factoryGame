using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorExecutor : MonoBehaviour
{
    float elapsed = 0f;

    public delegate void Executor();
    public static Executor executor;

    public void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed %= 1f;
            executor();

        }
    }

}

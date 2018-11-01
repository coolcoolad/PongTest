using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingleton<T> where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = new T();
            return instance;
        }
    }
}

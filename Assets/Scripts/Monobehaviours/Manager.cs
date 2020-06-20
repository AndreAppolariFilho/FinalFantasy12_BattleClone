using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    public static T Instance
    {
        get { return instance; }
        set
        {
            if(instance == null)
            {
                instance = value;
                DontDestroyOnLoad(instance.gameObject);
            }
            else if(instance!=value)
            {
                Destroy(instance.gameObject);
            }
        }
    }

    public void Awake()
    {
        Instance = this as T;
    }
}

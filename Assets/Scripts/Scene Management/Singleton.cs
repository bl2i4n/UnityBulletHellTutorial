using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    // this means this method is only visible to this class and any class that inherits from it
    protected virtual void Awake()
    {
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;
        }

        DontDestroyOnLoad(this.gameObject);

        if(!gameObject.transform.parent)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
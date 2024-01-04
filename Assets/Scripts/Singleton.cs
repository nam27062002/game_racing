﻿using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            DontDestroyOnLoad(_instance.gameObject);

            return _instance;
        }
    }


    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
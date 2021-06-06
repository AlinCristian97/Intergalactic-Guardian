using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer _instance;

    public static MusicPlayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MusicPlayer>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    
}

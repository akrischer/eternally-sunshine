using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLChecker : MonoBehaviour {

    public LevelManager levelManager;

    public AudioManager myAudioManager;

    private static DDOLChecker _instance;

    public static DDOLChecker Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("DDOLChecker").AddComponent<DDOLChecker>();
            }
            if (_instance.levelManager == null)
            {
                _instance.levelManager = GameObject.FindGameObjectWithTag(Tags.LEVEL_MANAGER).GetComponent<LevelManager>();
            }
            if (_instance.myAudioManager == null)
            {
                _instance.myAudioManager = GameObject.FindGameObjectWithTag(Tags.MY_AUDIO_MANAGER).GetComponent<AudioManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

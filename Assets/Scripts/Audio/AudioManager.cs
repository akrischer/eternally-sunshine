using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    List<InAudioEvent> backgroundMusicAudioEvents = new List<InAudioEvent>();
    LevelManager levelManager;

	void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag(Tags.LEVEL_MANAGER).GetComponent<LevelManager>();
        levelManager.AcceptLevelLoadedEvent(TriggerBGMEvent);
    }

    //string GetCurrentLevelBackgroundMusicName()
    //{
    //    LevelManager.Level currentLevel = levelManager.GetCurrentLevel();
    //    return currentLevel.Name + "_BGM";
    //}

    void TriggerBGMEvent()
    {
        // technically this triggers BEFORE the level is loaded :\
        int audioEventIndex = levelManager.GetLevelIndex(levelManager.GetNextLevel());
        InAudioEvent audioEvent = backgroundMusicAudioEvents[audioEventIndex];
        if (audioEvent != null)
        {
            InAudio.PostEvent(gameObject, audioEvent);
        }
        else
        {
            Debug.LogError("No background music audio event found for level '" + levelManager.GetNextLevel().Name + "'", gameObject);
        }
    }


}

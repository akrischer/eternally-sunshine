using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //[SerializeField]
    //List<InAudioEvent> backgroundMusicAudioEvents = new List<InAudioEvent>();
    [SerializeField]
    List<InMusicGroup> backgroundMusic = new List<InMusicGroup>();
    LevelManager levelManager;
    InAudio inAudioManager;

	void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag(Tags.LEVEL_MANAGER).GetComponent<LevelManager>();
        levelManager.AcceptLevelLoadedEvent(TriggerBGMEvent);
        inAudioManager = GameObject.FindGameObjectWithTag(Tags.INAUDIO_MANAGER).GetComponent<InAudio>();
        StartMusicForGameLaunch();
    }

    void StartMusicForGameLaunch()
    {
        InMusicGroup mainMenuMusic = backgroundMusic[0];
        InAudio.Music.PlayWithFadeIn(mainMenuMusic, mainMenuMusic.Volume, 3f);
    }

    void TriggerBGMEvent()
    {
        // technically this triggers BEFORE the level is loaded :\
        int nextLevelIndex = levelManager.GetLevelIndex(levelManager.GetNextLevel());
        /*InAudioEvent audioEvent = backgroundMusicAudioEvents[audioEventIndex];
        if (audioEvent != null)
        {
            InAudio.PostEvent(gameObject, audioEvent);
        }
        else
        {
            Debug.LogError("No background music audio event found for level '" + levelManager.GetNextLevel().Name + "'", gameObject);
        }*/
        InMusicGroup thisLevelBGM = backgroundMusic[nextLevelIndex - 1];
        InMusicGroup nextLevelBGM = backgroundMusic[nextLevelIndex];
        //InAudio.Music.CrossfadePlayStop(thisLevelBGM, nextLevelBGM, 5);

        // don't do any transition if their IDs are the same
        if (thisLevelBGM._ID != nextLevelBGM._ID)
        {
            InAudio.Music.FadeAndStop(thisLevelBGM, 1.5f);
            InAudio.Music.PlayWithFadeIn(nextLevelBGM, nextLevelBGM.Volume, 3f);
        }
    }

    string GetNameForBGM(LevelManager.Level level)
    {
        return level.Name + "_BGM";
    }


}

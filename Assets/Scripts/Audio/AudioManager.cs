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

    private float storedVolume = 1f;

	void Awake()
    {
        DDOLChecker ddolChecker = DDOLChecker.Instance;
        if (this != ddolChecker.myAudioManager)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag(Tags.LEVEL_MANAGER).GetComponent<LevelManager>();

        levelManager.AcceptLevelLoadedEvent(TriggerBGMEvent); // Transition BGM, if needed
        levelManager.AcceptLevelLoadedEvent(AddThisLevelsListener); // give InAudio each level's listener

        inAudioManager = GameObject.FindGameObjectWithTag(Tags.INAUDIO_MANAGER).GetComponent<InAudio>();
        StartMusicForGameLaunch();
        MuteAudio(); // only for web version
    }

    public void MuteAudio()
    {
        if (AudioListener.volume == 0f)
        {
            AudioListener.volume = storedVolume;
            storedVolume = 0f;
        } else
        {
            storedVolume = AudioListener.volume;
            AudioListener.volume = 0f;
        }
    }

    internal void StartMusicForGameLaunch()
    {
        InMusicGroup mainMenuMusic = backgroundMusic[0];
        InAudio.Music.PlayWithFadeIn(mainMenuMusic, mainMenuMusic.Volume, 3f);
    }

    void TriggerBGMEvent(LevelManager.Level previousLevel)
    {
        int currentLevelIndex = levelManager.GetLevelIndex(levelManager.GetCurrentLevel());
        int previousLevelIndex = levelManager.GetLevelIndex(previousLevel);
        /*InAudioEvent audioEvent = backgroundMusicAudioEvents[audioEventIndex];
        if (audioEvent != null)
        {
            InAudio.PostEvent(gameObject, audioEvent);
        }
        else
        {
            Debug.LogError("No background music audio event found for level '" + levelManager.GetNextLevel().Name + "'", gameObject);
        }*/
        InMusicGroup previousLevelBGM = backgroundMusic[previousLevelIndex];
        InMusicGroup currentLevelBGM = backgroundMusic[currentLevelIndex];
        //InAudio.Music.CrossfadePlayStop(thisLevelBGM, nextLevelBGM, 5);

        // don't do any transition if their IDs are the same
        if (previousLevelBGM._ID != currentLevelBGM._ID)
        {
            InAudio.Music.FadeAndStop(previousLevelBGM, 1.5f);
            InAudio.Music.PlayWithFadeIn(currentLevelBGM, currentLevelBGM.Volume, 3f);
        }
    }

    string GetNameForBGM(LevelManager.Level level)
    {
        return level.Name + "_BGM";
    }

    /// <summary>
    /// Every scene load, the InAudio's active listener is destroyed (bc it's the main camera). So we reset it on every load.
    /// </summary>
    private void AddThisLevelsListener(LevelManager.Level previousLevel)
    {
        if (inAudioManager.activeAudioListener == null)
        {
            inAudioManager.activeAudioListener = Camera.main.GetComponent<AudioListener>();
        }
    }

}

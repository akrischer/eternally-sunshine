using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSFX : MonoBehaviour
{

    static InAudio inAudioManager;

    private static AudioManager _audioManager;
    private static AudioManager AudioManager
    {
        get
        {
            if (_audioManager == null)
            {
                _audioManager = GameObject.FindGameObjectWithTag(Tags.MY_AUDIO_MANAGER).GetComponent<AudioManager>();
            }
            return _audioManager;
        }
    }

    public enum SFX
    {
        DOOR_OPENED_SFX,
        TEST_WALKING_FOOTSTEP
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        inAudioManager = GameObject.FindGameObjectWithTag(Tags.INAUDIO_MANAGER).GetComponent<InAudio>();
    }

    public static void PlaySFX(SFX sfx)
    {
        PlaySFX(sfx, inAudioManager.activeAudioListener.gameObject);
    }

    public static void PlaySFX(SFX sfx, GameObject target)
    {
        InAudioNode audioNode = InAudioUtil.GetAudioNodeByName(sfx.ToString());
        InAudio.Play(target, audioNode);
    }
}

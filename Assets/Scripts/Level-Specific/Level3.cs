using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Level3 : MonoBehaviour {

    private ThirdPersonUserControl _playerController;

    public RPGTalkManager rpgTalkManager;

    // Use this for initialization
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<ThirdPersonUserControl>();
        _playerController.DisablePlayerInput();
        StartCoroutine(BeginLevelSequence());
    }

    private IEnumerator BeginLevelSequence()
    {
        yield return new WaitForSeconds(2f);
        rpgTalkManager.PlayTalk("3");
    }


    public void DoorOpenedSequence()
    {
        StartCoroutine(_DoorOpenedSequence());
    }

    private IEnumerator _DoorOpenedSequence()
    {
        yield return new WaitForSeconds(2.2f);
        rpgTalkManager.PlayTalk("door_open");
    }
}

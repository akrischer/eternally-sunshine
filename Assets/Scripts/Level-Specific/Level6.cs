using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Level6 : MonoBehaviour
{
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
        yield return new WaitForSeconds(1f);
        rpgTalkManager.PlayTalk("6");
    }

}

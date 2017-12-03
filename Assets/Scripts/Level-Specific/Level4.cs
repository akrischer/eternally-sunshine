using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Level4 : MonoBehaviour
{

    private ThirdPersonUserControl _playerController;
    private ZoomEffect _zoomEffect;

    public RPGTalkManager rpgTalkManager;

    /// <summary>
    /// When the player enters the bridge area, what the camera will zoom to.
    /// </summary>
    public GameObject bridgeAreaZoomToObject;

    // Use this for initialization
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<ThirdPersonUserControl>();
        _playerController.DisablePlayerInput();
        _zoomEffect = Camera.main.GetComponent<ZoomEffect>();
        StartCoroutine(BeginLevelSequence());
    }

    private IEnumerator BeginLevelSequence()
    {
        yield return new WaitForSeconds(2f);
        rpgTalkManager.PlayTalk("4");
    }


    // When player enters area just past the glass
    public void RPGTalk_BridgeAreaStart()
    {
        _playerController.DisablePlayerInput();
        _zoomEffect.ZoomTo(bridgeAreaZoomToObject);
    }
    public void RPGTalk_BridgeAreaEnd()
    {
        _playerController.EnablePlayerInput();
        _zoomEffect.Reset();
    }

    // When bridge finishes moving, exposes the red button
    public void FinishedMovingBridge()
    {
        StartCoroutine(_FinishedMovingBridge());
    }

    private IEnumerator _FinishedMovingBridge()
    {
        _playerController.DisablePlayerInput();
        yield return new WaitForSeconds(2.3f);
        rpgTalkManager.PlayTalk("bridge_moved");
    }
}

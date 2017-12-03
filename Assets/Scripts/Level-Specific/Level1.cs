using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Level1 : MonoBehaviour {

    private GameObject _player;
    private ThirdPersonUserControl _playerController;

    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _playerController = _player.GetComponent<ThirdPersonUserControl>();

        _playerController.DisablePlayerInput();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

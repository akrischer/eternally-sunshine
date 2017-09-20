using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class ContinuousWalking : MonoBehaviour {

    private Vector3 startPosition;
    ThirdPersonCharacter thirdPersonCharacter;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        // get the third person character ( this should never be null due to require component )
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    }
	
	void FixedUpdate()
    {
        Vector3 moveVector = Vector3.forward;
        Vector3 reverseMoveVector = -moveVector;
        thirdPersonCharacter.Move(Vector3.forward, false, false);
        transform.position = startPosition;
    }
}

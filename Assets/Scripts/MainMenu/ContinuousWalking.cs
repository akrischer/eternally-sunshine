using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class ContinuousWalking : MonoBehaviour {

    public enum Direction
    {
        FORWARD, BACK
    }

    public Direction walkDirection = Direction.FORWARD;
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
        Vector3 walkDirectionVector = WalkDirectionToVector3(walkDirection);
        thirdPersonCharacter.Move(walkDirectionVector, false, false);
        transform.position = startPosition;
    }

    public void SetWalkDirection(Direction direction)
    {
        walkDirection = direction;
    }

    private Vector3 WalkDirectionToVector3(Direction dir)
    {
        switch(dir)
        {
            case Direction.FORWARD:
                return Vector3.forward;
            case Direction.BACK:
                return Vector3.back;
            default:
                return Vector3.zero;
        }
    }
}

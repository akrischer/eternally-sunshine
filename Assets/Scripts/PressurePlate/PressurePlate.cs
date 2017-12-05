using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    enum Direction { X, Y, Z }

    [SerializeField]
    float distance; // distance plate moves
    [SerializeField]
    float distancePerSecond; // time it takes to completely move plate
    [SerializeField]
    bool isRepeatable; // can you trigger this plate repeatedly?
    [SerializeField]
    bool isContinuous; // Trigger the end event repeatedly every fixed update?
    [SerializeField]
    Direction moveDirection = Direction.Y;
    [SerializeField]
    bool invert = false;

    private enum MoveDir
    {
        TOWARD_TRIGGER, AWAY_FROM_TRIGGER, STAY_PUT
    }
    [SerializeField]
    private MoveDir currentMoveDir = MoveDir.STAY_PUT;

    private Vector3 start;
    private bool hasFired; // used when isContinuous == false
    private bool isActive
    {
        get
        {
            float dot = Vector3.Dot(GetEndPosition() - transform.localPosition, Vector3.one);
            return invert ? dot <= 0 : dot >= 0;
        }
    }

    // called after plate reaches trigger position. If this plate is a continous trigger,
    // it will be called every fixed update.
    [SerializeField]
    UnityEvent Trigger;

    void Start()
    {
        start = gameObject.transform.localPosition;
    }

    void FixedUpdate()
    {
        if (isContinuous && isActive)
        {
            Trigger.Invoke();
        }

        switch(currentMoveDir)
        {
            case MoveDir.TOWARD_TRIGGER:
                // if plate has reached triggered position
                if (isActive)
                {
                    TryInvokeTrigger();
                    currentMoveDir = MoveDir.STAY_PUT;
                    transform.localPosition = GetEndPosition();
                }
                // if plate has not yet reached triggered position
                else
                {
                    Vector3 cp = transform.localPosition;
                    transform.Translate(Vector3.down * Time.fixedDeltaTime * distancePerSecond);
                }
                break;
            case MoveDir.AWAY_FROM_TRIGGER:
                // if plate has reached resting position
                Vector3 diff = start - transform.localPosition;
                float dot = Vector3.Dot(start - transform.localPosition, Vector3.one);
                if ((invert && dot >= 0) || (!invert && dot <= 0))
                {
                    currentMoveDir = MoveDir.STAY_PUT;
                    transform.localPosition = start;
                }
                else
                // if plate has not yet reached resting position
                {
                    Vector3 cp = transform.localPosition;
                    transform.Translate(Vector3.up * Time.fixedDeltaTime * distancePerSecond);
                }
                break;
        }
    }

    private float GetCurrentMovementAxisValue()
    {
        switch (moveDirection)
        {
            case Direction.X:
                return transform.localPosition.x;
            case Direction.Y:
                return transform.localPosition.y;
            case Direction.Z:
                return transform.localPosition.z;
            default:
                return transform.localPosition.y;
        }
    }

    private float GetStartMovementAxisValue()
    {
        switch (moveDirection)
        {
            case Direction.X:
                return start.x;
            case Direction.Y:
                return start.y;
            case Direction.Z:
                return start.z;
            default:
                return start.y;
        }
    }

    private Vector3 GetEndPosition()
    {
        float correctedDistance = invert ? -distance : distance;
        switch (moveDirection)
        {
            case Direction.X:
                return new Vector3(start.x - correctedDistance, start.y, start.z);
            case Direction.Y:
                return new Vector3(start.x, start.y - correctedDistance, start.z);
            case Direction.Z:
                return new Vector3(start.x, start.y, start.z - correctedDistance);
            default:
                return new Vector3(start.x, start.y - correctedDistance, start.z);
        }
    }

    void TryInvokeTrigger()
    {
        if (!hasFired)
        {
            hasFired = true;
            Trigger.Invoke();
        }
    }

    void OnTriggerEnter()
    {
        // don't do anything if it's already active
        if (!isActive)
        {
            currentMoveDir = MoveDir.TOWARD_TRIGGER;
        }
    }

    void OnTriggerExit()
    {
        if (isActive)
        {
            if (isRepeatable)
            {
                currentMoveDir = MoveDir.AWAY_FROM_TRIGGER;
                hasFired = false;
            } else
            {
                currentMoveDir = MoveDir.STAY_PUT;
            }
        } else
        {
            currentMoveDir = MoveDir.AWAY_FROM_TRIGGER;
        }
    }
}

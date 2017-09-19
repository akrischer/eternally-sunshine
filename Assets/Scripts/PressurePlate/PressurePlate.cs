using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField]
    float distance; // distance plate moves
    [SerializeField]
    float distancePerSecond; // time it takes to completely move plate
    [SerializeField]
    bool isRepeatable; // can you trigger this plate repeatedly?
    [SerializeField]
    bool isContinuous; // Trigger the end event repeatedly every fixed update?

    private enum MoveDir
    {
        TOWARD_TRIGGER, AWAY_FROM_TRIGGER, STAY_PUT
    }
    [SerializeField]
    private MoveDir currentMoveDir = MoveDir.STAY_PUT;

    private float startY;
    private bool hasFired; // used when isContinuous == false
    private bool isActive
    {
        get
        {
            return Mathf.Abs(transform.position.y - startY) >= distance;
        }
    }

    // called after plate reaches trigger position. If this plate is a continous trigger,
    // it will be called every fixed update.
    [SerializeField]
    UnityEvent Trigger;

    void adadad()
    {
        Debug.Log("TRIGGER!");
    }
    void Start()
    {
        startY = gameObject.transform.position.y;
        Trigger.AddListener(adadad);
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
                    Vector3 cp = transform.position;
                    cp.y = startY - distance;
                    transform.position = cp;
                }
                // if plate has not yet reached triggered position
                else
                {
                    Vector3 cp = transform.position;
                    transform.Translate(Vector3.down * Time.fixedDeltaTime * distancePerSecond);
                }
                break;
            case MoveDir.AWAY_FROM_TRIGGER:
                // if plate has reached resting position
                if (transform.position.y >= startY)
                {
                    currentMoveDir = MoveDir.STAY_PUT;
                    Vector3 cp = transform.position;
                    cp.y = startY;
                    transform.position = cp;
                }
                else
                // if plate has not yet reached resting position
                {
                    Vector3 cp = transform.position;
                    transform.Translate(Vector3.up * Time.fixedDeltaTime * distancePerSecond);
                }
                break;
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
            } else
            {
                currentMoveDir = MoveDir.STAY_PUT;
            }
        } else
        {
            currentMoveDir = MoveDir.STAY_PUT;
        }
    }
}

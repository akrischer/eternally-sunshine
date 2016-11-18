using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private Vector3 closedPos;

    [SerializeField]
    private Vector3 openPos;
    [SerializeField]
    private float openAndCloseDuration = 1f;

    private bool isMovementLocked = false;

    void Start()
    {
        closedPos = transform.localPosition;
    }

    public void TryOpen()
    {
        if (!isMovementLocked)
        {
            StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {
        isMovementLocked = true;
        // 1 step = 1/40 sec
        float stepTime = .02f;
        float startTime = Time.time;
        Vector3 totalMovementVector = openPos - closedPos;
        Vector3 dVector = totalMovementVector * stepTime / openAndCloseDuration;
        while (Time.time <= startTime + openAndCloseDuration)
        {
            transform.localPosition += dVector;
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = openPos;
        isMovementLocked = false;
    }

    public void TryClose()
    {
        if (!isMovementLocked)
        {
            StartCoroutine(Close());
        }
    }

    private IEnumerator Close()
    {
        isMovementLocked = true;
        // 1 step = 1/40 sec
        float stepTime = .02f;
        float startTime = Time.time;
        Vector3 totalMovementVector = closedPos - openPos;
        Vector3 dVector = totalMovementVector * stepTime / openAndCloseDuration;
        while (Time.time <= startTime + openAndCloseDuration)
        {
            transform.localPosition += dVector;
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = closedPos;
        isMovementLocked = false;
    }
}

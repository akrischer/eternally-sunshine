using UnityEngine;
using System.Collections;

public class CyclingSpotlight : MonoBehaviour {

    [SerializeField]
    float zStartDelta;
    [SerializeField]
    float zEndDelta;
    [SerializeField]
    float cycleDelay = 3;
    [SerializeField]
    float cycleDuration = 3;
    [SerializeField]
    bool doSnakeRepeat = false;

    // Used for snaking repeat patterns (e.g. light goes back and forth)
    private bool isOddCycle = false;

    Vector3 startPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        StartCycle();
	}

    public void StartCycle()
    {
        StartCoroutine(_StartCycle(doSnakeRepeat));
    }

    private IEnumerator _StartCycle(bool snakeRepeat = false)
    {
        yield return StartCoroutine(RunCycle(snakeRepeat));
        yield return new WaitForSeconds(cycleDelay);
        StartCoroutine(_StartCycle(snakeRepeat));
    }

    private IEnumerator RunCycle(bool snakeRepeat)
    {
        
        Vector3 start = startPos + new Vector3(0, 0, zStartDelta);
        Vector3 end = startPos + new Vector3(0, 0, zEndDelta);
        if (snakeRepeat && isOddCycle)
        {
            start = startPos + new Vector3(0, 0, zEndDelta);
            end = startPos + new Vector3(0, 0, zStartDelta);
        }

        float startTime = Time.time;
        
        transform.position = start;

        float timeStep = .02f;
        Vector3 dVector = (end - start) * timeStep / cycleDuration;
        while (Time.time <= startTime + cycleDuration)
        {
            transform.position += dVector;
            yield return new WaitForFixedUpdate();
        }

        transform.position = end;
        isOddCycle = !isOddCycle; // if was odd cycle, now even. And vice versa
    }

    #region Gizmos

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pos + new Vector3(0, 0, zStartDelta), .3f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos + new Vector3(0, 0, zEndDelta), .3f);
    }
    #endregion
}

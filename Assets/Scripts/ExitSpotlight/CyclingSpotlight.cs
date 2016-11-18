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

    Vector3 startPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        StartCycle();
	}

    public void StartCycle()
    {
        StartCoroutine(_StartCycle());
    }

    private IEnumerator _StartCycle()
    {
        yield return StartCoroutine(RunCycle());
        yield return new WaitForSeconds(cycleDelay);
        StartCoroutine(_StartCycle());
    }

    private IEnumerator RunCycle()
    {
        Vector3 start = startPos + new Vector3(0, 0, zStartDelta);
        Vector3 end = startPos + new Vector3(0, 0, zEndDelta);
        float startTime = Time.time;
        
        transform.position = start;

        float timeStep = .02f;
        Vector3 dVector = (end - start) * timeStep / cycleDuration;
        while (Time.time <= startTime + cycleDuration)
        {
            transform.position += dVector;
            yield return new WaitForFixedUpdate();
        }
        transform.position = startPos + new Vector3(0, 0, zEndDelta);
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

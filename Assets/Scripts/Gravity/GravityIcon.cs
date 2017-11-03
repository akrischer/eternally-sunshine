using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityIcon : MonoBehaviour {

    public float initialDelay;
    public float rotationTime;

    private static float intervalDelay = 2.5f;
    private Vector3 startRotationEuler;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpinCycle(initialDelay, rotationTime));
        startRotationEuler = transform.localEulerAngles;
	}
	
	private IEnumerator SpinCycle(float delay, float rotationTime)
    {
        yield return new WaitForSeconds(delay);

        Quaternion startRotation = transform.localRotation;
        float startTime = Time.time;

        while (Time.time - rotationTime <= startTime)
        {
            float deltaRotate = -180 * Time.deltaTime / rotationTime;
            transform.Rotate(deltaRotate, 0, 0, Space.Self);
            yield return null;
        }
        transform.localEulerAngles = startRotationEuler; 
        //transform.rotation = startRotation;
        StartCoroutine(SpinCycle(intervalDelay, rotationTime));
    }
}


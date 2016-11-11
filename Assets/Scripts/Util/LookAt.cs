using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    [SerializeField]
    Transform objectToFollow;

    void Start()
    {
        if (!objectToFollow)
        {
            Debug.Log(gameObject.name + " has no object to follow!", this);
        }
    }
	
	void Update () {
	    if (objectToFollow)
        {
            transform.LookAt(objectToFollow);
        }
	}
}

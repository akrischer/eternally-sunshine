using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    [SerializeField]
    Transform objectToFollow;

    private Spotlight spotlight;
    private float vertOffset;

    void Start()
    {
        spotlight = GetComponentInParent<Spotlight>();
        if (!objectToFollow)
        {
            Debug.Log(gameObject.name + " has no object to follow!", this);
        }
    }
	
	void Update () {
	    if (objectToFollow)
        {
            transform.LookAt(objectToFollow.position + new Vector3(0, spotlight.raycastVertOffset, 0));
        }
	}
}

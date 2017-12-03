using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAt : MonoBehaviour {

    [SerializeField]
    GameObject startAt;
    public bool useX, useY, useZ;

	// Use this for initialization
	void Start () {
        float x, y, z;
        x = useX ? startAt.transform.position.x : transform.position.x;
        y = useY ? startAt.transform.position.y : transform.position.y;
        z = useZ ? startAt.transform.position.z : transform.position.z;
	}

}

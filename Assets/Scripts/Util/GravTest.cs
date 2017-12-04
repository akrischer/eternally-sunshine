using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravTest : MonoBehaviour {

    public Vector3 gravXField = new Vector3(1,0,1);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GravityManager.mCamScaleVector_X = gravXField;
	}
}

using UnityEngine;
using System.Collections;

public class SpotlightEndpoint : MonoBehaviour {

	void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, .35f);
    }
}

using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour {

    private float radius = 3f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }

	void Update()
    {
        if (DebugManager.DebugModeOn)
        {
            DrawLineToPlayer();
        }

        transform.LookAt(player.transform);
    }

    #region Debug Stuff

    void DrawLineToPlayer()
    {
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
    }

    #endregion
}

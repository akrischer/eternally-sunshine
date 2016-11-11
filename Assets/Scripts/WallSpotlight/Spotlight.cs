using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour {

    private float radius = 3f;
    private GameObject player;
    [SerializeField]
    LayerMask layerMask;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }

	void Update()
    {
        if (DebugManager.DebugModeOn)
        {
            DrawCircleAroundPlayer();
        }

        transform.LookAt(player.transform);

        RaycastHit hit = RaycastToPlayer();
        if (hit.collider)
        {
            // do stuff
        }
    }

    #region Raycasting

    RaycastHit RaycastToPlayer()
    {
        if (DebugManager.DebugModeOn)
        {
            Debug.DrawRay(transform.position, GetDirectionVectorToPlayer(), Color.yellow, 1);
        }
        Ray ray = new Ray(transform.position, GetDirectionVectorToPlayer());
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, GetDistanceToPlayer() + 1, layerMask);
        return raycastHit;
    }
    #endregion

    #region Debug Stuff

    void DrawLineToPlayer()
    {
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
    }

    void DrawCircleAroundPlayer()
    {
        DebugExtension.DebugCircle(player.transform.position, Mathf.Sqrt(GetDistanceToPlayer()) * .3f);
    }

    #endregion

    float GetDistanceToPlayer()
    {
        return Vector3.Magnitude(player.transform.position - transform.position);
    }

    Vector3 GetDirectionVectorToPlayer(bool normalized = false)
    {
        Vector3 v = player.transform.position - transform.position;
        return normalized ? v.normalized : v;
    }
}

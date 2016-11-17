using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour {

    private float radius = 3f;
    private GameObject player;
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    private ColoredLight.LightColor lightColor;
    private ColoredLight coloredLight;
    private PlayerColorManager playerColorManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        coloredLight = new ColoredLight(lightColor, GetSpotlightColor());
        playerColorManager = GameObject.FindWithTag(Tags.PLAYER_COLOR_MANAGER).GetComponent<PlayerColorManager>();
    }

	void FixedUpdate()
    {
        if (DebugManager.DebugModeOn)
        {
            DrawCircleAroundPlayer();
        }

        transform.LookAt(player.transform);

        RaycastHit hit = RaycastToPlayer();
        if (hit.collider)
        {
            if (hit.collider.tag == Tags.PLAYER)
            {
                playerColorManager.AcceptLight(coloredLight);
            } else if (hit.collider.tag == Tags.LIGHTPAD_TRIGGER)
            {
                LightTriggerPad triggerPad = hit.collider.GetComponent<LightTriggerPad>();
                if (triggerPad)
                {
                    triggerPad.AcceptLight(coloredLight);
                }
            }

        }
    }

    #region Raycasting

    RaycastHit RaycastToPlayer()
    {
        if (DebugManager.DebugModeOn)
        {
            Debug.DrawRay(transform.position, GetDirectionVectorToPlayer(), Color.yellow, 1);
            Vector3 coneUp = Vector3.Lerp(GetDirectionVectorToPlayer(true), transform.up, (3f / 90f)) * 100;
            Debug.DrawRay(transform.position, coneUp, Color.green, 0);
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


    /// <summary>
    /// Assumes Color determined by child object with SpotlightWallObj tag
    /// </summary>
    /// <returns></returns>
    Color GetSpotlightColor()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == Tags.SPOTLIGHT_WALL_OBJ)
            {
                return child.GetComponent<Renderer>().material.color;
            }
        }
        return Color.white;
    }
}

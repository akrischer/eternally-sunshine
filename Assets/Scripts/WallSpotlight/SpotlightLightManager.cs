using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Light))]
public class SpotlightLightManager : MonoBehaviour {

    private GameObject spotlightWallObj;
    private Light light;
    private Renderer spotlightWallObjRenderer;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        light.type = LightType.Spot;

        foreach (Transform child in transform.parent)
        {
            if (child.tag == Tags.SPOTLIGHT_WALL_OBJ)
            {
                spotlightWallObj = child.gameObject;
                break;
            }
        }

        spotlightWallObjRenderer = spotlightWallObj.GetComponent<Renderer>();
        Color spotlightAlbedo = spotlightWallObjRenderer.material.color;
        SetSpotlightColor(spotlightAlbedo);
	}
	
	// Update is called once per frame
	void Update () {
	    if (GetSpotlightColor() != GetSpotlightWallObjColor())
        {
            SetSpotlightColor(GetSpotlightWallObjColor());
        }
	}

    Color GetSpotlightColor()
    {
        return light.color;
    }

    Color GetSpotlightWallObjColor()
    {
        return spotlightWallObjRenderer.material.color;
    }

    void SetSpotlightColor(Color color)
    {
        light.color = color;
    }
}

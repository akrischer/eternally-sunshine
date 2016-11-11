using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerColorManager : MonoBehaviour {

    [SerializeField]
    Color playerColor;

    [SerializeField]
    GameObject playerModelBody;
    Renderer playerRenderer;

    private List<Light> _spotlights;
    private List<Light> Spotlights
    {
        get
        {
            if (_spotlights == null)
            {
                _spotlights = GameObject.FindGameObjectsWithTag(Tags.SPOTLIGHT_LIGHT)
                    .Select(go => go.GetComponent<Light>())
                    .ToList();
            }
            return _spotlights;
        }
    }

	// Use this for initialization
	void Start () {
        playerColor = CalculatePlayerColor();
        playerModelBody = GameObject.FindGameObjectWithTag(Tags.PLAYER_MODEL_BODY);
        playerRenderer = playerModelBody.GetComponent<Renderer>();
        UpdatePlayerColor();
    }
	
	// Update is called once per frame
	void Update () {
        if (CalculatePlayerColor() != GetPlayerColor())
        {
            UpdatePlayerColor();
        }
	}

    void UpdatePlayerColor()
    {
        playerColor = CalculatePlayerColor();
        Material playerMat = playerRenderer.material;

        playerMat.SetColor("_Color", playerColor);
        playerMat.SetColor("_OutlineColor", GetInvertedPlayerColor());

        playerRenderer.material = playerMat;
    }

    Color GetInvertedPlayerColor()
    {
        float h, s, v;
        Color.RGBToHSV(playerColor, out h, out s, out v);
        h = (h + 0.5f) % 1.0f;
        return Color.HSVToRGB(h, s, v);
    }

    // Additively blend all spotlight colors
    Color CalculatePlayerColor()
    {
        List<Color> spotlightColors = Spotlights.Select(spotlight => spotlight.color).ToList();
        return spotlightColors.Aggregate((prod, next) => AdditivelyBlendColors(prod, next));
    }

    Color GetPlayerColor()
    {
        return playerRenderer.material.color;
    }

    Color AdditivelyBlendColors(Color c1, Color c2)
    {
        return (c1 + c2) / 2;
    }
}

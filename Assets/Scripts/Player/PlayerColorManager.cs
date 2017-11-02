using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerColorManager : MonoBehaviour {

    [SerializeField]
    Color playerColor;
    [SerializeField]
    Color defaultColor;

    GameObject playerModelBody;
    Renderer playerRenderer;

    // A rolling list of what lights are currently shining on player
    List<ColoredLight> lightsOnPlayer = new List<ColoredLight>();

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
        if (lightsOnPlayer.Count > 0)
        {
            return lightsOnPlayer
            .Select(light => light.Color)
            .Aggregate((prod, next) => AdditivelyBlendColors(prod, next));
        } else
        {
            return defaultColor;
        }

    }

    Color GetPlayerColor()
    {
        return playerRenderer.material.color;
    }

    Color AdditivelyBlendColors(Color c1, Color c2)
    {
        return (c1 + c2) / 2;
    }

    #region Test

    /// <summary>
    /// Accept that a ColoredLight is currently hitting this Light Trigger Pad.
    /// This does not guarantee that the pad will be charged, as the pad will only charge if exactly
    /// the colors in acceptedColors are currently hitting the pad
    /// </summary>
    /// <param name="coloredLight"></param>
    public void AcceptLight(ColoredLight coloredLight)
    {
        StartCoroutine(_AcceptLight(coloredLight));
    }

    // Add color to lightsOnTriggerPad
    // Wait for 2 fixed updates, to allow other colors to hit this pad (that we can then track)
    private IEnumerator _AcceptLight(ColoredLight coloredLight)
    {
        lightsOnPlayer.Add(coloredLight);

        // We wait for two fixed updates to allow for minimal overlap between adding/removing from the lightsOnTriggerPad list
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        lightsOnPlayer.Remove(coloredLight);
    }

    #endregion
}

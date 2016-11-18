using UnityEngine;
using System.Collections;

public struct ColoredLight {

	public enum LightColor
    {
        BLUE, RED
    }

    private LightColor lightColor;
    public LightColor ColorName
    {
        get
        {
            return lightColor;
        }
    }

    private Color color;
    public Color Color
    {
        get
        {
            return color;
        }
    }

    public ColoredLight(LightColor colorName, Color color)
    {
        this.lightColor = colorName;
        this.color = color;
    }
}

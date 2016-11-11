using UnityEngine;
using System.Collections;

public struct ColoredLight {

	public enum LightColor
    {
        BLUE, RED
    }

    private LightColor lightColor;
    public LightColor Color
    {
        get
        {
            return lightColor;
        }
    }

    public ColoredLight(LightColor color)
    {
        this.lightColor = color;
    }
}

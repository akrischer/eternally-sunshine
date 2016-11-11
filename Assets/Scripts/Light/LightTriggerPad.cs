using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LightTriggerPad : MonoBehaviour {

    [SerializeField]
    ColoredLight.LightColor acceptColor;
    [SerializeField]
    bool unlocked = false;
    [SerializeField]
    float currentCharge;
    [SerializeField]
    UnityEvent Unlocked;

    public ColoredLight.LightColor AcceptColor
    {
        get
        {
            return acceptColor;
        }
    }

    public float CurrentCharge
    {
        get
        {
            return currentCharge;
        }
    }

    public bool IsUnlocked
    {
        get
        {
            return unlocked;
        }
    }

    void FixedUpdate()
    {
        if (!unlocked)
        {
            unlocked = currentCharge == 100;
            if (unlocked)
            {
                Unlocked.Invoke();
            }
        }
        if (!unlocked)
        {
            AcceptChargeAmount(-1);
        }
    }

    /// <summary>
    /// Incrementally charges this light pad, if the colored light matches this pad's accept color
    /// </summary>
    /// <param name="coloredLight"></param>
    /// <returns>whether the coloredLight matches this pad's accept color</returns>
    public bool AcceptCharge(ColoredLight coloredLight)
    {
        bool colorMatch = coloredLight.Color == AcceptColor;
        if (colorMatch)
        {
            AcceptChargeAmount(1.5f);
        }
        return colorMatch;
    }

    private void AcceptChargeAmount(float amount)
    {
        if (!unlocked)
        {
            currentCharge += amount;
            currentCharge = Mathf.Clamp(currentCharge, 0, 100);
        }
    }

}

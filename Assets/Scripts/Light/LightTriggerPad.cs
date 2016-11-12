using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LightTriggerPad : MonoBehaviour {
    [SerializeField]
    float chargePerTick = .5f;
    [SerializeField]
    float decayPerTick = 1f;
    [SerializeField]
    bool unlocked = false;
    float currentCharge;
    [SerializeField]
    UnityEvent Unlocked;
    [SerializeField]
    List<ColoredLight.LightColor> acceptedColors; // what specific set of colors will trigger this pad?

    // A rolling list of what lights are currently on this pad
    List<ColoredLight.LightColor> lightsOnTriggerPad = new List<ColoredLight.LightColor>();


    public List<ColoredLight.LightColor> AcceptedColors
    {
        get
        {
            return acceptedColors;
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
            AcceptChargeAmount(-decayPerTick);
        }
    }

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
        lightsOnTriggerPad.Add(coloredLight.Color);
        
        // We wait for two fixed updates to allow for minimal overlap between adding/removing from the lightsOnTriggerPad list
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        if (ListsContainSameElements(acceptedColors, lightsOnTriggerPad))
        {
            // we add decayPerTick, because we decay the amount even if we're adding to the charge
            // we need to divide by the number of elements in acceptedColors, because AcceptChargeAmount will be
            // triggered once per color that's in acceptedColors.
            float chargeAmount = (decayPerTick + chargePerTick) / acceptedColors.Count;
            AcceptChargeAmount(chargeAmount);
        }
        lightsOnTriggerPad.Remove(coloredLight.Color);
    }

    private void AcceptChargeAmount(float amount)
    {
        if (!unlocked)
        {
            currentCharge += amount;
            currentCharge = Mathf.Clamp(currentCharge, 0, 100);
        }
    }

    /// <summary>
    /// Does a "set check" for two lists. I.E. given two lists A, B, return if
    /// all elements in A are in B, and all elements in B are in A
    /// </summary>
    /// <returns></returns>
    private bool ListsContainSameElements<T>(List<T> a, List<T> b)
    {
        return a.All(aElem => b.Contains(aElem)) &&
            b.All(bElem => a.Contains(bElem));
    }

}

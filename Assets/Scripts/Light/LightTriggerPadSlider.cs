using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LightTriggerPadSlider : MonoBehaviour {

    Slider slider;
    [SerializeField]
    LightTriggerPad sliderFor;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = sliderFor.CurrentCharge / 100f;
	}
}

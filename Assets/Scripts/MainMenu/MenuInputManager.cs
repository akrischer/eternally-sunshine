using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MenuInputManager : MonoBehaviour {

    Timer inputLockoutTimer;
    public float moveInDirectionThreshold = 0.65f;
    public float menuTransitionLockoutSeconds = 1.2f; // how much time input is locked out for when a valid input is given
    [SerializeField]
    MenuOption currentOption;

    public enum MenuDirection
    {
        LEFT, RIGHT, UP, DOWN
    }

	// Use this for initialization
	void Start () {
        inputLockoutTimer = new Timer(menuTransitionLockoutSeconds);
	}

    public void SetCurrentOption(MenuOption option)
    {
        currentOption = option;
    }
	
	// Update is called once per frame
	void Update () {
        if (inputLockoutTimer.IsComplete())
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            
            if (CrossPlatformInputManager.GetButtonDown("Submit"))
            {
                currentOption.TryInvoke();
                inputLockoutTimer.StartTimer();
            } else if (Mathf.Abs(h) >= moveInDirectionThreshold || Mathf.Abs(v) >= moveInDirectionThreshold)
            {
                // use the input with the stronger magnitude
                if (Mathf.Abs(h) >= Mathf.Abs(v))
                {
                    MenuDirection direction = h > 0 ? MenuDirection.RIGHT : MenuDirection.LEFT;
                    currentOption.TryGoInMenuDirection(direction);
                } else
                {
                    MenuDirection direction = v > 0 ? MenuDirection.UP : MenuDirection.DOWN;
                    currentOption.TryGoInMenuDirection(direction);
                }
                inputLockoutTimer.StartTimer();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

	void PlayFootstepSFX()
    {
        AudioSFX.PlaySFX(AudioSFX.SFX.TEST_WALKING_FOOTSTEP, gameObject);
    }
}

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private bool canAcceptInput = true;
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        private float _h, _v;
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        public void EnablePlayerInput()
        {
            canAcceptInput = true;
        }

        public void DisablePlayerInput()
        {
            canAcceptInput = false;
        }


        private void Update()
        {
            if (!m_Jump && canAcceptInput)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Joystick_Y");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            

            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            _h = h;
            _v = v;
            bool crouch = Input.GetKey(KeyCode.C);

            if (!canAcceptInput)
            {
                _h = h = 0;
                _v = v = 0;
                crouch = false;
            }

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                m_CamForward = Vector3.Scale(m_Cam.forward, GravityManager.mCamScaleVector).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
                m_Move = Vector3.ProjectOnPlane(m_Move, Vector3.up);
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }

            //}
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift) && canAcceptInput) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            DebugExtension.DebugArrow(transform.position + Vector3.up, m_Move);
            m_Jump = false;
        }


    }
}

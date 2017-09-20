using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public List<Rotation> rotations;

	void FixedUpdate()
    {
        foreach (Rotation rotation in rotations)
        {
            Vector3 rotationVector = Vector3.zero;
            switch(rotation.rotationAxis)
            {
                case Rotation.RotationAxis.X:
                    rotationVector = Vector3.right;
                    break;
                case Rotation.RotationAxis.Y:
                    rotationVector = Vector3.up;
                    break;
                case Rotation.RotationAxis.Z:
                    rotationVector = Vector3.forward;
                    break;
            }
            transform.Rotate(rotationVector * rotation.degreesPerSecond * Time.fixedDeltaTime);
        }
    }

    [System.Serializable]
    public class Rotation
    {
        public enum RotationAxis
        {
            X, Y, Z
        }

        public RotationAxis rotationAxis;
        public float degreesPerSecond;
    }
}

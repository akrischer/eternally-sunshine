using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    List<Camera> cameras = new List<Camera>();

    void Start()
    {
        SetActiveCamera(cameras[0].name);
    }

    public void SetActiveCamera(string name)
    {
        Camera newActiveCamera = cameras.Where(cam => cam.gameObject.name == name)
            .First();

        if (newActiveCamera != null)
        {
            // disable all cameras
            cameras.ForEach(cam => cam.enabled = false);
            newActiveCamera.enabled = true;
        } else
        {
            Debug.LogWarning("Cannot disable camera '" + name + "'. No such camera found.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    List<Camera> cameras = new List<Camera>();
    private Camera activeCamera;
    private GameObject levelSelectCanvas;

    void Start()
    {
        SetActiveCamera(cameras[0].name);
        levelSelectCanvas = GameObject.FindGameObjectWithTag(Tags.LEVEL_SELECT_CANVAS);
        levelSelectCanvas.SetActive(false);
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
            activeCamera = newActiveCamera;
        } else
        {
            Debug.LogWarning("Cannot disable camera '" + name + "'. No such camera found.");
        }
    }

    public Camera GetActiveCamera()
    {
        return activeCamera;
    }

    public void EnableLevelSelectCanvas()
    {
        levelSelectCanvas.SetActive(true);
    }

    public void DisableLevelSelectCanvas()
    {
        levelSelectCanvas.SetActive(false);
    }
}

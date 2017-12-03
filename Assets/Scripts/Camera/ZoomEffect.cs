using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomEffect : MonoBehaviour {

    public float zoomTime = 2f;
    public float deltaZoom = -20f;

    private ZoomSettings _savedZoomSettings;
    private Camera _camera;

	// Use this for initialization
	void Start () {
        _camera = GetComponent<Camera>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Exposed Methods
    public void ZoomTo(GameObject obj)
    {
        // Save zoom settings for RESET
        _savedZoomSettings = new ZoomSettings(_camera);
        StartCoroutine(_ZoomTo(obj));
    }

    public void Reset()
    {
        //_camera.fieldOfView = _savedZoomSettings.fov;
        //transform.position = _savedZoomSettings.position;
        //transform.rotation = _savedZoomSettings.rotation;
        StartCoroutine(_Reset());
    }
    #endregion

    #region Private (Hidden) Methods

    private IEnumerator _Reset()
    {
        float time = Time.time;
        Quaternion endRotation = _savedZoomSettings.rotation;
        Quaternion startRotation = transform.rotation;
        float startFov = _camera.fieldOfView;
        float endFov = _savedZoomSettings.fov;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = _savedZoomSettings.position;
        while (transform.rotation != endRotation || _camera.fieldOfView != endFov || transform.position != endPosition)
        {
            float t = Time.time - time;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            _camera.fieldOfView = Mathf.Lerp(startFov, endFov, t);
            transform.position = Vector3.Slerp(startPosition, endPosition, t);
            yield return null;
        }
    }

    private IEnumerator _ZoomTo(GameObject obj)
    {
        float time = Time.time;
        Quaternion endRotation = Quaternion.LookRotation(obj.transform.position - transform.position);
        float endZoom = _camera.fieldOfView + deltaZoom;
        Quaternion neededRotation = Quaternion.LookRotation(obj.transform.position - transform.position);
        while (Time.time - time < zoomTime)
        {
            float dZoom = (deltaZoom * Time.deltaTime) / zoomTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime);
            _camera.fieldOfView += dZoom;
            yield return null;
        }
        //transform.rotation = endRotation;
        //_camera.fieldOfView = endZoom;
    }

    public class ZoomSettings
    {
        public float fov;
        public Vector3 position;
        public Quaternion rotation;

        public ZoomSettings(Camera camera)
        {
            this.fov = camera.fieldOfView;
            this.position = camera.transform.position;
            this.rotation = camera.transform.rotation;
        }
    }
    #endregion
}

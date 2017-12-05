using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour {

    public GameObject endCreditsPosition;
    public float unitsPerSecond = 100f;
    public float delay = 3f;

    private bool move = false;
    private RectTransform rectTransform;
    private RectTransform endRectTransform;

    private LevelManager _levelManager;
    private AudioManager _audioManager;

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
        endRectTransform = endCreditsPosition.GetComponent<RectTransform>();
        StartCoroutine(Sequence());

        _levelManager = GameObject.FindGameObjectWithTag(Tags.LEVEL_MANAGER).GetComponent<LevelManager>();
	}

    private IEnumerator Sequence()
    {
        yield return new WaitForSeconds(delay);
        move = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (move && !IsFinished())
        {
            rectTransform.localPosition += new Vector3(0, unitsPerSecond * Time.deltaTime, 0);
        }
	}

    private bool IsFinished()
    {
        return rectTransform.localPosition.y >= endRectTransform.localPosition.y;
    }

    public void GoBackToMainMenu()
    {
        _levelManager.LoadLevel("main_menu");
    }

    //void OnEnable()
    //{
    //    _audioManager = GameObject.FindGameObjectWithTag(Tags.MY_AUDIO_MANAGER).GetComponent<AudioManager>();
    //    SceneManager.sceneLoaded += StartMusicAgain;
    //}
    //
    //void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= StartMusicAgain;
    //}
    //
    //void StartMusicAgain(Scene scene, LoadSceneMode mode)
    //{
    //    _audioManager.StartMusicForGameLaunch();
    //}
}

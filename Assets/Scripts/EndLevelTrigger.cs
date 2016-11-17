using UnityEngine;
using System.Collections;

public class EndLevelTrigger : MonoBehaviour {

    LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.FindWithTag(Tags.LEVEL_MANAGER).GetComponent<LevelManager>();
    }

	public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.PLAYER)
        {
            levelManager.LoadNextLevel();
        }
    }
}

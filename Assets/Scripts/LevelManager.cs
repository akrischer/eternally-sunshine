using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private List<Level> levels;
    private int currentLevelPtr;

    [System.Serializable]
    public class Level
    {
        public string name;
    }

    void Start()
    {
        currentLevelPtr = levels.FindIndex(level => level.name == SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(GetNextLevel().name);
    }

	private Level GetNextLevel()
    {
        if (currentLevelPtr + 1 < levels.Count)
        {
            currentLevelPtr++;
            
        }
        return levels[currentLevelPtr];
    }


}

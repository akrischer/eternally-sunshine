using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private AllLevels allLevels;

    [SerializeField]
    List<string> scenesInBuild = new List<string>();

    LevelLoadedEvent levelLoaded = new LevelLoadedEvent();

    private bool isLoading = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        allLevels = GetAllLevels();

        // load all scenes available
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }
    }

    public void AcceptLevelLoadedEvent(UnityAction<Level> action)
    {
        levelLoaded.AddListener(action);
    }

    public void RemoveLevelLoadedEvent(UnityAction<Level> action)
    {
        levelLoaded.AddListener(action);
    }

    public void CompleteQuitGame()
    {
        Debug.Log("QUITTING GAME");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        LoadLevel(GetNextLevel().Name);
    }

    public void LoadLevel(string levelName)
    {
        if (scenesInBuild.Contains(levelName) && !isLoading)
        {
            StartCoroutine(_LoadLevel(levelName));
        }
        else if (!isLoading)
        {
            Debug.LogError("Cannot load level '" + levelName + "'. No level found with that name.");
        }
    }

    private IEnumerator _LoadLevel(string levelName)
    {
        isLoading = true;
        Level currentLevel = GetCurrentLevel();
        AsyncOperation async = SceneManager.LoadSceneAsync(allLevels.GetLevelByName(levelName).Name);
        yield return async;
        isLoading = false;
        levelLoaded.Invoke(currentLevel);
    }

	public Level GetNextLevel()
    {
        return allLevels.GetLevelAfter(GetCurrentLevel());
    }

    public int GetLevelIndex(Level level)
    {
        return allLevels.GetLevelIndex(level);
    }

    private AllLevels GetAllLevels()
    {
        TextAsset levels = Resources.Load<TextAsset>("levels");
        AllLevels loadedAllLevels = JsonUtility.FromJson<AllLevels>(levels.text);
        Resources.UnloadAsset(levels);
        return loadedAllLevels;
    }

    internal Level GetCurrentLevel()
    {
        return allLevels.GetLevelByName(SceneManager.GetActiveScene().name);
    }

    #region Level Loaded Event
    public class LevelLoadedEvent : UnityEvent<Level>
    {

    }
    #endregion
    #region JSON Serialization
    [System.Serializable]
    public class Level
    {
        [SerializeField]
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        public override bool Equals(object obj)
        {
            Level other = obj as Level;

            return !Object.ReferenceEquals(null, other) &&
                name.Equals(other.name);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override string ToString()
        {
            return "[" + name + "]";
        }
    }

    [System.Serializable]
    public class AllLevels
    {
        [SerializeField]
        List<Level> levels;

        public Level GetLevelAfter(Level level)
        {
            int idx = levels.IndexOf(level);

            // Get next level
            if (idx + 1 < levels.Count)
            {
                return levels[idx + 1];
            }
            // Else return current level
            else
            {
                return level;
            }
        }

        public Level GetLevelByName(string name)
        {
            return levels.Where(l => name.Equals(l.Name)).First();
        }

        public int GetLevelIndex(Level level)
        {
            return levels.IndexOf(level);
        }
    }
    #endregion
}

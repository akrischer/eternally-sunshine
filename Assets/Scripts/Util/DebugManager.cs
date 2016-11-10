using UnityEngine;
using System.Collections;

public class DebugManager : MonoBehaviour {

    [SerializeField]
    private bool debugModeOn;

    private static DebugManager _instance;
    private static DebugManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<DebugManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public static bool DebugModeOn
    {
        get
        {
            return _Instance.debugModeOn;
        }
    }
}

using UnityEngine;
using System.Collections;

public class GameObjectEnabler : MonoBehaviour {

    public GameObject go;

    public void Enable()
    {
        go.SetActive(true);
    }

    public void Disable()
    {
        go.SetActive(false);
    }
}

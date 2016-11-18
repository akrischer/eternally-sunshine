using UnityEngine;
using System.Collections;

public class ComponentEnabler : MonoBehaviour
{

    public string type;

	public void Enable()
    {
        (GetComponent(System.Type.GetType(type)) as MonoBehaviour).enabled = true;
    }

    public void Disable()
    {
        (GetComponent(System.Type.GetType(type)) as MonoBehaviour).enabled = false;
    }
}

using UnityEngine;
using System.Collections;

public class Slidable : MonoBehaviour {

    enum AxisConstraint
    {
        X, Y, Z
    };

    [SerializeField]
    private AxisConstraint axisConstraint;
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject end;

    private Vector3 Start
    {
        get
        {
            return start.transform.position;
        }
    }
    private Vector3 End
    {
        get
        {
            return end.transform.position;
        }
    }

    void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        Vector3 newPos = new Vector3(0,0,0);

        switch (axisConstraint)
        {
            case AxisConstraint.X:
                Vector3 unclampedVector = new Vector3(pos_move.x, transform.position.y, transform.position.z);
                newPos = ClampVector(unclampedVector, Start, End);
                break;
            case AxisConstraint.Y:
                unclampedVector = new Vector3(transform.position.x, pos_move.y, transform.position.z);
                newPos = ClampVector(unclampedVector, Start, End);
                break;
            case AxisConstraint.Z:
                unclampedVector = new Vector3(transform.position.x, transform.position.y, pos_move.z);
                newPos = ClampVector(unclampedVector, Start, End);
                break;
        }
        //transform.position = newPos;
        transform.parent.position = newPos;
    }   

    Vector3 ClampVector(Vector3 val, Vector3 min, Vector3 max)
    {
        return new Vector3(
                Mathf.Clamp(val.x, min.x, max.x),
                Mathf.Clamp(val.y, min.y, max.y),
                Mathf.Clamp(val.z, min.z, max.z)
            );
    }
}

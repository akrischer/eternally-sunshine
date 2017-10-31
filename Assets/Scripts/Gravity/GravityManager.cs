using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityManager : MonoBehaviour
{

    
    public enum GravityDirection
    {
        X, Y, Z
    }

    private Stack<GravityDirection> rotationStack = new Stack<GravityDirection>();
    GameObject player;
    GameObject gameWorld;
    private static GravityDirection currentGravityDirection = GravityDirection.Y;
    [SerializeField]
    private static Vector3 mCamScaleVector_X = new Vector3(-1,0,1), mCamScaleVector_Y = new Vector3(1, 0, 1), mCamScaleVector_Z = new Vector3(1, 0, -1);
    private static Vector3 gameWorldEulerAngles_X = new Vector3(0,0,-90), gameWorldEulerAngles_Y = new Vector3(0, 0, 0), gameWorldEulerAngles_Z = new Vector3(90, 0, 0);
    public static Vector3 mCamScaleVector
    {
        get
        {
            switch (currentGravityDirection)
            {
                case GravityDirection.X:
                    return mCamScaleVector_X;
                case GravityDirection.Y:
                    return mCamScaleVector_Y;
                case GravityDirection.Z:
                    return mCamScaleVector_Z;
                default:
                    return mCamScaleVector_Y;
            }
        }
    }

    private float gravMagnitude = Physics.gravity.y;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        gameWorld = GameObject.FindGameObjectWithTag(Tags.GAME_WORLD);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGravityDirection(string gravity)
    {
        Vector3 currentGameWorldRotation = gameWorld.transform.eulerAngles;
        Vector3 playerRotationVector = player.transform.eulerAngles;

        UndoGravityRotations();

        switch (gravity)
        {
            case "X":
                RotateDirection(GravityDirection.X);
                rotationStack.Push(GravityDirection.X);
                break;
            case "Y":
                RotateDirection(GravityDirection.Y);
                rotationStack.Push(GravityDirection.Y);
                break;
            case "Z":
                RotateDirection(GravityDirection.Z);
                rotationStack.Push(GravityDirection.Z);
                break;
        }
    }

    private void UndoGravityRotations()
    {
        while (rotationStack.Count > 0)
        {
            GravityDirection gravityDirection = rotationStack.Pop();
            RotateDirection(gravityDirection, true);
        }
    }

    private void RotateDirection(GravityDirection direction, bool reverse = false)
    {
        Vector3 currentGameWorldRotation = gameWorld.transform.eulerAngles;
        Vector3 playerRotationVector = player.transform.eulerAngles;


        switch (direction.ToString())
        {
            case "X":
                currentGravityDirection = GravityDirection.X;
                float z = reverse ? 0 : -90;
                RotateAround(player.transform.position, gameWorld, new Vector3(0, 0, z));
                break;
            case "Y":
                currentGravityDirection = GravityDirection.Y;
                RotateAround(player.transform.position, gameWorld, new Vector3(0, 0, 0));
                break;
            case "Z":
                currentGravityDirection = GravityDirection.Z;
                float x = reverse ? 0 : 90;
                RotateAround(player.transform.position, gameWorld, new Vector3(x, 0, 0));
                break;
        }
    }

    private void RotateAround(Vector3 point, GameObject obj, Vector3 endRotation)
    {
        float x, y, z;
        Quaternion objRotation = obj.transform.rotation;
        x = endRotation.x - objRotation.eulerAngles.x;
        gameWorld.transform.RotateAround(player.transform.position, obj.transform.right, x);

        y = endRotation.y - objRotation.eulerAngles.y;
        gameWorld.transform.RotateAround(player.transform.position, obj.transform.up, y);

        z = endRotation.z - objRotation.eulerAngles.z;
        obj.transform.RotateAround(point, obj.transform.forward, z);


    }
}

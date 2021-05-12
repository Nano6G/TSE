using UnityEngine;

//simple script to create an Array of transforms for the spawn manager
// could need reworking to make multiple paths but suitable for testing
public class WaypointsScript : MonoBehaviour
{
    public static Transform[] points;
    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);

        }
    }
}


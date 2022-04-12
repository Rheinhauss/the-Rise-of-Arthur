using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagEnum
{
    None,
    Red,
    Green,
    Blue,
    Mixed
}

public class DrawManager :MonoBehaviour
{
    [SerializeField] private static GameObject Laser;
    public static Color GetColorFromTag(TagEnum tag)
    {
        switch (tag)
        {
            case TagEnum.Red:
                return Color.red;
            case TagEnum.Green:
                return Color.green;
            case TagEnum.Blue:
                return Color.blue;
            case TagEnum.Mixed:
                return Color.black;
            default:
                return Color.white;
        }
    }

    public static GameObject DrawLaser(Vector3 origin, Vector3 end)
    {
        if(Laser == null)
        {
            Laser = Resources.Load("LaserBeamTut01") as GameObject;
        }
        Vector3 dir = end - origin;
        GameObject laser = Instantiate(Laser, origin, Quaternion.identity);
        laser.transform.LookAt(dir);
        laser.GetComponentInChildren<LineRenderer>().SetPosition(1, new Vector3(0, 0, dir.magnitude));
        return laser;
    }
}

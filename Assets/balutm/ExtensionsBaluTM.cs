using UnityEngine;
using System.Collections;

public static class ExtensionsBaluTM
{
    /// <summary>
    /// sets the layer to all childrens and grand childrens upto 1 Level deep
    /// </summary>
    public static void SetLayer(this GameObject parent, int layer, bool includeChildren = true)
    {
        parent.layer = layer;
        if (includeChildren)
        {
            foreach (Transform trans in parent.transform.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = layer;
                //again for theire childerens to One Level..
                foreach (Transform trans2 in trans.GetComponentsInChildren<Transform>(true))
                {
                    trans2.gameObject.layer = layer;
                }

            }
        }
    }


    /// <summary>
    /// get distance without Y Axis and without sqaureRoot, use like if (DistanceBtmSuperFast(p1,p2) lessthen closeDistance * closeDistance)
    /// </summary>
    public static float DistanceBtmSuperFast(this Vector3 tempPosition, Vector3 targetPosition)
    {
        float xD = targetPosition.x - tempPosition.x;
        float zD = targetPosition.z - tempPosition.z;
        return (xD * xD) + (zD * zD);
        //PhotonNetwork.room.PlayerCount
    }


    /// <summary>
    /// get distance without sqaureRoot, use like if (DistanceBtmFast(p1,p2) lessthen closeDistance * closeDistance)
    /// </summary>
    public static float DistanceBtmFast(this Vector3 tempPosition, Vector3 targetPosition)
    {
        Vector3 offset = tempPosition - targetPosition;
        return offset.sqrMagnitude;
    }

    public static void logMyPos(this GameObject target, Object contextt)
    {
        Vector3 mypos = target.transform.position;
        Debug.Log("x=" + mypos.x + " y=" + mypos.y + " z=" + mypos.z, contextt);
    }

    public static void logMyPos(this Transform target, Object contextt)
    {
        Vector3 mypos = target.position;
        Debug.Log("x=" + mypos.x + " y=" + mypos.y + " z=" + mypos.z, contextt);
    }

    public static string getMyPosString(this Transform target)
    {
        Vector3 mypos = target.position;
        return "x=" + mypos.x + " y=" + mypos.y + " z=" + mypos.z;
    }

    public static string logMyPos(this Vector3 mypos)
    {
        return "x=" + mypos.x + " y=" + mypos.y + " z=" + mypos.z;
    }


    //Int toString() Optimization..

    // Lookup table.
    static string[] _cache =
    {
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "11",
        "12",
        "13",
        "14",
        "15",
        "16",
        "17",
        "18",
        "19",
        "20",
        "21",
        "22",
        "23",
        "24",
        "25",
        "26",
        "27",
        "28",
        "29",
        "30",
        "31",
        "32",
        "33",
        "34",
        "35",
        "36",
        "37",
        "38",
        "39",
        "40",
        "41",
        "42",
        "43",
        "44",
        "45",
        "46",
        "47",
        "48",
        "49",
        "50"
    };

    // Lookup table last index.
    const int _top = 49;
    public static string ToStringBtmFast(this int value)
    {
        // See if the integer is in range of the lookup table.
        // ... If it is present, return the string literal element.
        if (value >= 0 &&
            value <= _top)
        {
            return _cache[value];
        }
        // Fall back to ToString method.
        return value.ToString();
    }


   


    public const string Key_FoodCount = "fdck";
    public const string Key_Dying = "dngk";

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBounds : MonoBehaviour
{
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;

    float minx
    {
        get { return topLeft.position.x; }
    }

    float maxx
    {
        get { return bottomRight.position.x; }
    }

    float miny
    {
        get { return bottomRight.position.y; }
    }

    float maxy
    {
        get { return topLeft.position.y; }
    }

    public Vector2 BoundPt(Vector2 pt)
    {
        return new Vector2(
            Mathf.Clamp(pt.x, minx, maxx),
            Mathf.Clamp(pt.y, miny, maxy)
        );
    }
}
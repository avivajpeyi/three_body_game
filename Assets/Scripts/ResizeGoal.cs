using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeGoal : MonoBehaviour
{

    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    private EdgeCollider2D col;

    private void Start()
    {
        col = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        Resize();
    }

    void Resize()
    {
        Vector2 p0 = start.position;
        Vector2 p1 = end.position;
        Vector2 c = 0.5f * (p0 + p1);
        
        Vector2 a = Vector2.left;
        Vector2 b = Vector2.right;
        
        gameObject.transform.position = c;
        col.points = new []{p0-c,p1-c};
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GravitationalBody gb;
    public float moveSpeed = 5;
    private Rigidbody2D rb;
    public int score = 0;
    
    public bool mouseController = true;
    public bool alive = true;

    public Vector2 spawnPt; 
    
    private void Start()
    {
        gb = GetComponent<GravitationalBody>();
        rb = GetComponent<Rigidbody2D>();
        Die();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPt, 0.5f);
    }

    public void LerpToPos(Vector3 p)
    {
        p.z = transform.position.z;
        rb.MovePosition(Vector2.Lerp(transform.position, p,
            Time.deltaTime * moveSpeed));
    }

    Vector3 GetMousePt()
    {
        Vector3 mousePt = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        return Camera.main.ScreenToWorldPoint(mousePt);
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            if (mouseController)
            {
                LerpToPos(GetMousePt());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && alive)
        {
            Die();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
            score += 1;
    }


    public void Die()
    {
        alive = false;
        gb.enabled = false;
    }

    public void ResetPlayer(Vector2 pt)
    {
        spawnPt = pt;
        score = 0;
        alive = true;
        gb.enabled = true;
    }
}
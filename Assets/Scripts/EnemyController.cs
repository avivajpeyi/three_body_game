using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isBouncing;

    public Vector3 initVel;

    [SerializeField] private float repulsiveBounceFactor = 15f;

    public GameObject sparksOnCollision;
    
    void Start()
    {
        isBouncing = false;
        rb = GetComponent<Rigidbody2D>();
        
        if (initVel != Vector3.zero)
            rb.velocity = initVel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(sparksOnCollision, transform.position, Quaternion.identity);
        
        if( collision.gameObject.CompareTag("Enemy") && !isBouncing)
        {
            
            Rigidbody2D rbcol = collision.rigidbody;
            rb.AddForce(collision.contacts[0].normal * repulsiveBounceFactor);
            rbcol.AddForce(collision.contacts[0].normal * -repulsiveBounceFactor);
            
            isBouncing = true;
            Invoke("StopBounce", 0.3f);
//            Debug.Log("Special bounce triggered");
        }
    }
    void StopBounce()
    {
        isBouncing = false;
    }
}

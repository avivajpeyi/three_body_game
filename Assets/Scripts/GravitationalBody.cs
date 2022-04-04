using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class GravitationalBody : MonoBehaviour
{
	public float maxDistance = 1000;
	public float startingMass = 10;
	public float drag = 0;

	public float fudge = 10f;
	public float maxSpeed = 20f;
	
	private Rigidbody2D rb; 
	public List<Rigidbody2D> attractableRb;
	

	void Start()
	{
		SetupRigidbody2D();
	}

	void SetupRigidbody2D()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0f;
		rb.drag = drag;
		rb.mass = startingMass;
		rb.angularDrag = 0f;
	}

	void FixedUpdate()
	{

		for (int i =0; i < attractableRb.Count; i++)
		{
			var otherBody = attractableRb[i];

			if (otherBody == null)
				continue;

			//We arn't going to add a gravitational pull to our own body
			if (otherBody == rb)
				continue;

			// gravitationally pull the other body 
			otherBody.AddForce(DetermineGravitationalForce(otherBody));

		}
		
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

		
	}
	

	/// <summary>
	///
	/// F = G mm unit(r) / r**2
	/// </summary>
	/// <param name="otherBody"></param>
	/// <returns></returns>
	Vector2 DetermineGravitationalForce(Rigidbody2D otherBody)
	{
		Vector2 relativePosition = rb.position - otherBody.position;
		float distance = Mathf.Clamp(relativePosition.magnitude, 0.1f, maxDistance);
		float mag = fudge *  rb.mass * otherBody.mass  /  Mathf.Pow(distance, 0);
		Vector2 dir = relativePosition.normalized;
		return mag * dir;
	}
	

	
	
}

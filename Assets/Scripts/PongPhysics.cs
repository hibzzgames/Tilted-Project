using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPhysics : MonoBehaviour
{
	[Header("Pong Properties")]
	[Tooltip("The speed at which the pong travels")]
	[SerializeField]
	private float Speed = 5;

	[Header("Paddle Properties")]
	[Tooltip("Amount of angular displacement applied if pong hits on the edge of the paddle")]
	[SerializeField]
	private float AdditionalAngularDisplacement = 30.0f;

	private Vector3 expectedRotation;

	private void FixedUpdate()
	{
		transform.position += transform.right * Time.deltaTime * Speed;

		// TODO: Remove this (doesn't belong here)
		if(transform.position.y < -10.0f)
		{
			GetComponent<SetSpawnLocation>().ResetPosition();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// Check if the collider is tagged as an object that reflects the pong
		if (other.CompareTag("Wall") || other.CompareTag("Paddle"))
		{
			// Get a reflected vector with other's right as the normal of reflection
			Vector3 reflectedVector = Vector3.Reflect(transform.right, other.transform.right);

			// get the direction of this vector
			float angle =  Mathf.Atan2(reflectedVector.y, reflectedVector.x);
			
			// the angle needs to be converted to degrees, and is set as the expected 
			// resolved rotation 
			expectedRotation.z = Mathf.Rad2Deg * angle;
        }

		// if the object is tagged paddle, perform additional calculation
		if (other.CompareTag("Paddle"))
		{
			float displacement = transform.position.x - other.transform.position.x;
			expectedRotation.z -= displacement * AdditionalAngularDisplacement * 2.0f / other.transform.localScale.y;
		}

		transform.rotation = Quaternion.Euler(expectedRotation);
	}
}

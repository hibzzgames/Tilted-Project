using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongCollision : MonoBehaviour
{
	[Header("Paddle Properties")]
	[Tooltip("Amount of angular displacement applied if pong hits on the edge of the paddle")]
	[SerializeField]
	private float AdditionalAngularDisplacement = 15.0f;

	// cache of expected rotation of the pong
	private Vector3 expectedRotation;

	private void Awake()
	{
		reloadExpectedRotation();
	}

	// Todo: somehow call this function on forced rotation change
	/// <summary>
	/// Reloads the expected rotation to comply with the new rotation set externaly
	/// </summary>
	private void reloadExpectedRotation()
	{
		expectedRotation = transform.rotation.eulerAngles;
	}

	private void OnTriggerEnter(Collider other)
	{
		// Check if the collider is tagged as an object that reflects the pong
		if (other.CompareTag("Wall") || other.CompareTag("Paddle"))
		{
			// Get a reflected vector with other's right as the normal of reflection
			Vector3 reflectedVector = Vector3.Reflect(transform.right, other.transform.right);

			// get the direction of this vector
			float angle = Mathf.Atan2(reflectedVector.y, reflectedVector.x);

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

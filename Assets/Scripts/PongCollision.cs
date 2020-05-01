using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongCollision : MonoBehaviour
{
	[Header("Paddle Properties")]
	[Tooltip("Amount of angular displacement applied if pong hits on the edge of the paddle")]
	[SerializeField]
	private float AngularDisplacement = 45.0f;

	#region Event declarations

	// When a pong and a wall collides this event is triggered
	public delegate void WallPongCollisionAction();
	public static event WallPongCollisionAction OnPongWallCollision;

	// When a pong and the paddle collides this event is triggered
	public delegate void PaddlePongCollisionAction();
	public static event PaddlePongCollisionAction OnPongPaddleCollision;

	#endregion

	// cache of expected rotation of the pong
	private Vector3 expectedRotation;

	private void Awake()
	{
		reloadExpectedRotation();
	}

	// Note: call this function on forced rotation change
	/// <summary>
	/// Reloads the expected rotation to comply with the new rotation set externaly
	/// </summary>
	private void reloadExpectedRotation()
	{
		expectedRotation = transform.rotation.eulerAngles;
	}

	private void OnTriggerEnter(Collider other)
	{
		// Check if the collider is tagged as a wall that reflects the pong
		if (other.CompareTag("Wall"))
		{
			// Get a reflected vector with other's right as the normal of reflection
			Vector3 reflectedVector = Vector3.Reflect(transform.right, other.transform.right);

			// get the direction of this vector
			float angle = Mathf.Atan2(reflectedVector.y, reflectedVector.x);

			// the angle needs to be converted to degrees, and is set as the expected 
			// resolved rotation 
			expectedRotation.z = Mathf.Rad2Deg * angle;

			// Trigger Wall Pong collision event
			OnPongWallCollision?.Invoke();
		}

		// if the object is tagged paddle, perform calculation based on where the pong hit the paddle
		if (other.CompareTag("Paddle"))
		{
			// displacement is calculated to be between -1 and 1
			float displacement = 2 * (transform.position.x - other.transform.position.x) / other.transform.localScale.y;
			expectedRotation.z = 90 - displacement * (90 - AngularDisplacement);

			// Trigger Paddle pong collision event
			OnPongPaddleCollision?.Invoke();
		}

		transform.rotation = Quaternion.Euler(expectedRotation);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPhysics : MonoBehaviour
{
	[Header("Pong Properties")]
	[Tooltip("The rate at which speed reduces every second")]
	[SerializeField]
	private float speedReductionRate = 2;

	[Tooltip("The maximum speed of the pong")]
	[SerializeField]
	private float MaxSpeed = 10;

	[Tooltip("The minimum speed of the pong")]
	[SerializeField]
	private float MinSpeed = 8;

	/// <summary>
	/// The speed at which the pong travels
	/// </summary>
	private float Speed = 5;


	#region default unity functions

	private void OnEnable()
	{
		PongCollision.OnPongPaddleCollision += SetSpeedToMax;
	}

	private void OnDisable()
	{
		PongCollision.OnPongPaddleCollision -= SetSpeedToMax;
	}

	private void Update()
	{
		// reduce speed by the given reduction rate, and clamp it between min and max speeds
		Speed -= speedReductionRate * Time.deltaTime;
		Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

		// move the pong with the given speed
		transform.position += transform.right * Time.deltaTime * Speed;
	}

	#endregion

	#region public functions

	/// <summary>
	/// Sets the speed to Max
	/// </summary>
	public void SetSpeedToMax()
	{
		Speed = MaxSpeed;
	}
	

	#endregion

	#region private functions

	#endregion
}

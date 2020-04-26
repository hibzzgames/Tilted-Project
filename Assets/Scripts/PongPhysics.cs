using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPhysics : MonoBehaviour
{
	[Header("Pong Properties")]
	[Tooltip("The speed at which the pong travels")]
	[SerializeField]
	private float Speed = 5;

	private void FixedUpdate()
	{
		transform.position += transform.right * Time.deltaTime * Speed;
	}
}

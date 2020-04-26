using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
	[Tooltip("Area in which the orb can spawn")]
	[SerializeField]
	private Vector2 SpawnZone = new Vector2();

	[Header("Advanced")]
	[Tooltip("Preferred Z-axis of an orb")]
	[SerializeField]
	private float PreferredZAxis = 1;

	/// <summary>
	/// Local cache of orb's position
	/// </summary>
	private Vector3 orbPosition = new Vector3();

	private void Awake()
	{
		// Set the prefered at the start of the scene
		orbPosition.z = PreferredZAxis;
	}

	private void OnEnable()
	{
		// Choose random x and y position withi the spawn zone 
		// and set it as orb's position
		orbPosition.x = Random.Range(-SpawnZone.x, SpawnZone.x);
		orbPosition.y = Random.Range(-SpawnZone.y, SpawnZone.y);

		// Make the object's position the new orb's position
		transform.position = orbPosition;
	}
}

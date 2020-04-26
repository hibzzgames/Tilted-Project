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

    private Vector3 orbPosition = new Vector3();

    private void OnEnable()
    {
        orbPosition.x = Random.Range(-SpawnZone.x, SpawnZone.x);
        orbPosition.y = Random.Range(-SpawnZone.y, SpawnZone.y);
        orbPosition.z = PreferredZAxis;

        transform.position = orbPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollision : MonoBehaviour
{
    /// <summary>
    /// On orb collision event
    /// </summary>
    public delegate bool OrbCollisionAction();
    public static event OrbCollisionAction OnOrbCollision;

    /// <summary>
    /// Orb collision event with a reference to the gameobject
    /// </summary>
    /// <param name="gameObject"> The orb in general </param>
    public delegate bool OrbCollisionDestroyAction(GameObject gameObject);
    public static event OrbCollisionDestroyAction OnOrbCollisionDestroy;

    private void OnTriggerEnter(Collider other)
    {
        // If a pong collides with an orb
        if(other.CompareTag("Pong"))
        {
            // Sends two messages, one with gameobject reference and another empty
            OnOrbCollisionDestroy(gameObject);
            OnOrbCollision();
        }  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollision : MonoBehaviour
{
    /// <summary>
    /// On orb collision event
    /// </summary>
    public delegate bool OrbCollectionAction();
    public static event OrbCollectionAction OnOrbCollected;

    /// <summary>
    /// Orb collision event with a reference to the gameobject
    /// </summary>
    /// <param name="gameObject"> The orb in general </param>
    public delegate bool OrbCollectionGameobjectAction(GameObject gameObject);
    public static event OrbCollectionGameobjectAction OnOrbCollectedGameObjectReference;

    /// <summary>
    /// Private reference to orb's health
    /// </summary>
    private OrbHealth orbHealth = null;

    private void Start()
    {
        orbHealth = GetComponent<OrbHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If a pong collides with an orb
        if(other.CompareTag("Pong"))
        {
            // if health was greater than zero, that means that the pong can be collected
            if(orbHealth.GetCurrentHealth() > 0)
            {
                // Sends two messages, one with gameobject reference and another empty
                OnOrbCollectedGameObjectReference?.Invoke(gameObject);
                OnOrbCollected?.Invoke();
            }
        }  
    }
}

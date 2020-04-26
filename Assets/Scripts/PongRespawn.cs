using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongRespawn : MonoBehaviour
{
    [Tooltip("The pong is considered safe within the given dimension")]
    [SerializeField]
    private Vector2 SafeBoxDimensions = new Vector2();

    void Update()
    {
        // If the object beyond the Safe Box's dimensions
        if (transform.position.y < -SafeBoxDimensions.y ||
            transform.position.y > SafeBoxDimensions.y ||
            transform.position.x < -SafeBoxDimensions.x ||
            transform.position.x > SafeBoxDimensions.x)
        {
            // the object gets reset
            GetComponent<SetSpawnLocation>().ResetPosition();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaddleInput : MonoBehaviour
{
    [Header("Paddle properties")]
    [Tooltip("[Tooltip not defined yet]")]
    [SerializeField]
    private float AngleSensitivity = 0.01f;

    [SerializeField]
    private TextMeshProUGUI debugText;

    private float RotationRateY;

    private Vector3 expectedPosition;

    private void Start()
    {
        expectedPosition = transform.position;
    }

    // When the object is enabled
    private void OnEnable()
    {
        // Enable the attitude sensor
        Input.gyro.enabled = true;
    }

    // When the object is disabled
    private void OnDisable()
    {
        // Disable the attitude sensor
        Input.gyro.enabled = false;
    }

    private void FixedUpdate()
    {
        RotationRateY = Input.gyro.rotationRate.y;

        expectedPosition.x += RotationRateY * AngleSensitivity;
        expectedPosition.x = Mathf.Clamp(expectedPosition.x, -2.8125f, 2.8125f);

        debugText.text = "Attitude: " + Input.gyro.attitude.eulerAngles
            + "\nGravity: " + Input.gyro.gravity
            + "\nRotation Rate: " + Input.gyro.rotationRate
            + "\nExpected Position X: " + expectedPosition.x;

        transform.position = expectedPosition;
    }
}

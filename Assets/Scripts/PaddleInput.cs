using UnityEngine;

public class PaddleInput : MonoBehaviour
{
	[Header("Paddle properties")]
	[Tooltip("[Tooltip not defined yet]")]
	[SerializeField]
	private float PaddleSensitivity = 5.0f;

#if !DEBUG
	[Tooltip("The amount of motion ignored by the paddle")]
	[SerializeField]
	private float PaddleResponsiveness = 0.1f;
#endif

	/// <summary>
	/// Rotation rate on the y axis of the device
	/// </summary>
	private float rotationRateY;

	/// <summary> 
	/// Expected position of the paddle at the next frame 
	/// </summary>
	private Vector3 expectedPosition;

	private float ClampDistance = 2.8125f;

	// Called at the start of the scene
	private void Start()
	{
		// Set expected position to be the transform's initial position
		expectedPosition = transform.position;

		// Calculate clamp distance based on screen
		ClampDistance = Screen.width * Camera.main.orthographicSize / Screen.height;
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

	// Called once every frame
	private void Update()
	{
		// Get the rotation rate in the y axis of the device
		rotationRateY = Input.gyro.rotationRate.y;

#if !DEBUG
		// ignore moving the paddle if the intensity is not high enough
		if (rotationRateY < PaddleResponsiveness && rotationRateY > -PaddleResponsiveness) { return; }
#endif

#if DEBUG
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			expectedPosition.x -= PaddleSensitivity * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			expectedPosition.x += PaddleSensitivity * Time.deltaTime;
		}

#endif

		// Apply the rotation rate along with the paddle speed to calculate expected position.
		// Expected position is clamped based on the screen size.
		expectedPosition.x += rotationRateY * PaddleSensitivity * Time.deltaTime;
		expectedPosition.x = Mathf.Clamp(expectedPosition.x, -ClampDistance, ClampDistance);

		// Set the object's position to the expected position
		transform.position = expectedPosition;
	}
}

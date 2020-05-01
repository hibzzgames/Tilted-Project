using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnforceAspectRatio : MonoBehaviour
{
	[Header("Screen Properties")]
	[Tooltip("The aspect ratio that needs to be fixed")]
	[SerializeField]
	private Vector2 TargetRatio = new Vector2(9,16);

	public delegate void ScreenRescaleAction ();
	public static event ScreenRescaleAction OnScreenRescale;

	private void Start()
	{
		// Rescales the camera on start
		RescaleCamera();
	}

	/// <summary>
	/// Rescales the camera to the set camera given camera target ratio
	/// </summary>
	void RescaleCamera()
	{
		// Get the aspect as float for the target and screen
		float targetAspect = TargetRatio.y / TargetRatio.x;
		float windowAspect = (float) Screen.width / (float) Screen.height;
		
		// scale height would be the ratio between the two
		float scaleHeight = windowAspect / targetAspect;

		// Attempt to get the camera
		Camera camera = GetComponent<Camera>();

		// Log error message and return if no camera was found
		if(camera == null)
		{
			Debug.LogError("[" + GetType() + "] " + MethodBase.GetCurrentMethod().Name + " cannot find a camera component in [" + gameObject.name + "]");
			return;
		}

		// if the scale height is less than 1, handle it based on scale height
		if(scaleHeight < 1.0f)
		{
			camera.rect = new Rect(0, (1 - scaleHeight) / 2.0f, 1, scaleHeight);
			StaticInformation.ScreenHeight = scaleHeight * Screen.height;
			StaticInformation.ScreenWidth = Screen.width;
		}
		// if the scale height is greater than 1, handle it based on scale width
		else
		{
			float scaleWidth = 1 / scaleHeight;
			camera.rect = new Rect((1 - scaleWidth) / 2, 0, scaleWidth, 1);
			StaticInformation.ScreenWidth = scaleWidth * Screen.width;
			StaticInformation.ScreenHeight = Screen.height;
		}

		OnScreenRescale?.Invoke();
	}

	// TOOD: Add <c> public void RescaleCamera (Vector2 AspectRation) </c> overloaded function
}

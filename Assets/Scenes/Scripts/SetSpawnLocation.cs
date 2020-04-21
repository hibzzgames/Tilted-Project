// File authored by Hibnu Hishath for hibzz.games

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set's the spawn location of an object based on the given basic screen anchors
/// </summary>
public class SetSpawnLocation : MonoBehaviour
{
    #region Exposed variables
    [Header("Horizontal Properties")]
	[Tooltip("The Horizontal position relative to screen at which the object has to spawn")]
	[SerializeField]
	private HorizontalLocation HorizontalAnchor = 0;

	[Tooltip("The offset applied relative to the given horizontal anchor")]
	[SerializeField]
	private float HorizontalOffset = 0;

	[Header("Vertical Properties")]
	[Tooltip("The vertical position relative to screen at which the object has to spawn")]
	[SerializeField]
	private VerticalLocation VerticalAnchor = 0;

	[Tooltip("The offset applied relative to the given vertical anchor")]
	[SerializeField]
	private float VerticalOffset = 0;

	[Header("Advanced properties")]
	[Tooltip("The preferred Z position override for the final transform. " +
		"Used for collisions and handling clipping planes")]
	[SerializeField]
	private float preferredZPosition = 1;

    #endregion

    /// <summary>
    /// Function called at the very first frame of the scene
    /// </summary>
    private void Awake()
	{
		float horizontalPosition = 0;
		float verticalPosition = 0;


		// Calculating the screen's horizontal position based on given anchor
		if (HorizontalAnchor == HorizontalLocation.Center)
		{
			horizontalPosition = Screen.width / 2;
		}
		else if(HorizontalAnchor == HorizontalLocation.Right)
		{
			horizontalPosition = Screen.width;
		}

		// Calculating the screen's vertical position based on given anchor
		if(VerticalAnchor == VerticalLocation.Center)
		{
			verticalPosition = Screen.height / 2;
		}
		else if(VerticalAnchor == VerticalLocation.Top)
		{
			verticalPosition = Screen.height;
		}

		// create a screen position vector
		Vector3 screenPosition = new Vector3(horizontalPosition, verticalPosition, 0);

		// convert items to world space and apply to gameobject's position
		Vector3 worldPosition  = Camera.main.ScreenToWorldPoint(screenPosition);

		// Apply offsets and set object to clipping plane 1
		worldPosition.x += HorizontalOffset;
		worldPosition.y += VerticalOffset;
		worldPosition.z = preferredZPosition;

		// Set the gameobject's position as the new world position
		transform.position = worldPosition;
	}

	#region public screenspace enums
	/// <summary>
	/// Horizontal location on screen
	/// </summary>
	public enum HorizontalLocation
	{
		Center,
		Left,
		Right
	}

	/// <summary>
	/// Vertical location on screen
	/// </summary>
	public enum VerticalLocation
	{
		Center,
		Top,
		Bottom
	}
	#endregion
}

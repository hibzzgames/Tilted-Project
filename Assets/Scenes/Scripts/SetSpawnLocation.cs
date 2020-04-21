// File authored by Hibnu Hishath for hibzz.games

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnLocation : MonoBehaviour
{
	[Header("Horizontal Properties")]
	[Tooltip("The Horizontal position relative to screen at which the object has to spawn")]
	public HorizontalLocation HorizontalPosition;

	[Tooltip("The offset applied relative to the given horizontal position")]
	public float HorizontalOffset = 0;

	[Header("Vertical Properties")]
	[Tooltip("The vertical position relative to screen at which the object has to spawn")]
	public VerticalLocation VerticalPosition;

	[Tooltip("The offset applied relative to the given vertical position")]
	public float VerticalOffset = 0;

	/// <summary>
	/// Function called at the very first frame of the scene
	/// </summary>
	private void Awake()
	{
		
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

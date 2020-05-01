using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbContentDimmer : MonoBehaviour
{
	[Tooltip("The default value of the object to dim when an event occurs")]
	[Range(0, 1)]
	[SerializeField]
	private float DefaultDimValue = 0.5f;

	/// <summary>
	/// List of materials cache'd for easier access
	/// </summary>
	private List<Material> materials = new List<Material>();

	private void Start()
	{
		// Loops through the object to get a list of child materials tagged "Orb Asset"
		foreach(Transform child in transform)
		{
			if(child.tag == "OrbAsset")
			{
				// adds it to the material list
				materials.Add(child.GetComponent<Renderer>().material);
			}
		}
	}

	private void OnEnable()
	{
		// Link event handlers 
		OrbHealth.OnOrbZeroHealth += OrbDefaultDim;
		PongCollision.OnPongPaddleCollision += OrbReset;
	}

	private void OnDisable()
	{
		// unlink event handlers
		OrbHealth.OnOrbZeroHealth -= OrbDefaultDim;
		PongCollision.OnPongPaddleCollision -= OrbReset;
	}

	/// <summary>
	/// Dims an orb by the given dim value
	/// </summary>
	/// <param name="dimValue"> A value between 0 and 1 representing the alpha of the orb </param>
	public void OrbDim(float dimValue)
	{
		// clamps the value between 0 and 1 for safety
		dimValue = Mathf.Clamp(dimValue, 0, 1);

		// sets the alpha of each material to the given dimValue
		foreach(Material material in materials)
		{
			material.SetFloat("_alpha", dimValue);
		}
	}

	/// <summary>
	/// Resets the orb to no transparency
	/// </summary>
	public void OrbReset()
	{
		// Sets each materials alpha to 1.0f
		foreach(Material material in materials)
		{
			material.SetFloat("_alpha", 1.0f);
		}
	}

	/// <summary>
	/// Dims the orb by the given <c> DefaultDimValue </c>
	/// </summary>
	public void OrbDefaultDim()
	{
		// Call orb dim with the default value
		OrbDim(DefaultDimValue);
	}
}

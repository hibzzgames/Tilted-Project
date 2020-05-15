using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLifeBarHandler : MonoBehaviour
{
	[Tooltip("A reference to the paddle life that the bar has to represent")]
	[SerializeField]
	private PaddleLife PaddleLifeReference = null;

	/// <summary>
	/// Cache of the local scale variable of the object's transform
	/// </summary>
	private Vector3 localScale;

	private void Start()
	{
		// At start, gets the local scale of the transform, and sets it to a private cache
		localScale = transform.localScale;
	}

	private void OnEnable()
	{
		// Hook the handlers
		PaddleLife.OnPaddleHealthUpdated += UpdateHealthBar;
	}

	private void OnDisable()
	{
		// Unhook handlers
		PaddleLife.OnPaddleHealthUpdated -= UpdateHealthBar;
	}

	/// <summary>
	/// On request, updates the health bar with the current currentlife and max life valuess 
	/// </summary>
	void UpdateHealthBar()
	{
		// Calculates the health bar length based on % of current health on max health 
		localScale.x = (float) PaddleLifeReference.GetCurrentHealth() / PaddleLifeReference.GetMaxHealth();
		
		// Updates the health bar with the newly calculated local scale value
		transform.localScale = localScale;
	}
}

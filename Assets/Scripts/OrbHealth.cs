using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHealth : MonoBehaviour
{
	[Tooltip("The health with which each orb is spawned in / resets to")]
	[SerializeField]
	private int MaxHealth = 3;

	/// <summary>
	/// The current health status of an orb
	/// </summary>
	private int CurrentHealth = 0;

	#region Event Declarations

	public delegate void OrbZeroHealthAction();
	public static event OrbZeroHealthAction OnOrbZeroHealth;

	#endregion

	#region Unity Functions

	private void Start()
	{
		// Reset's health on start of the orb spawn
		ResetHealth();
	}

	private void OnEnable()
	{
		// Event handlers
		PongCollision.OnPongWallCollision += ReduceHealth;
		PongCollision.OnPongPaddleCollision += ResetHealth;
	}

	private void OnDisable()
	{
		// Disable event handlers
		PongCollision.OnPongWallCollision -= ReduceHealth;
		PongCollision.OnPongPaddleCollision -= ResetHealth;
	}

	#endregion

	/// <summary>
	/// Resets the health of the orb back to full
	/// </summary>
	public void ResetHealth()
	{
		CurrentHealth = MaxHealth;
	}

	/// <summary>
	/// Reduces health of the orb by 1
	/// </summary>
	public void ReduceHealth()
	{
		ReduceHealth(1);
	}

	/// <summary>
	/// Reduces health of the orb by the given damage value
	/// </summary>
	/// <param name="damage"> The damage value to apply </param>
	public void ReduceHealth(int damage)
	{
		CurrentHealth -= damage;

		// if current health is less than or equal to zero, trigger an orb-health-zero event
		if(CurrentHealth <= 0)
		{
			OnOrbZeroHealth?.Invoke();
		}
	}

	/// <summary>
	/// Drains all the health of an orb immediately
	/// </summary>
	public void DrainHealth()
	{
		CurrentHealth = 0;
		OnOrbZeroHealth?.Invoke();
	}

	/// <summary>
	/// Gets the current health of the orb
	/// </summary>
	/// <returns> The current health of the orb </returns>
	public int GetCurrentHealth()
	{
		return CurrentHealth;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TODO: Remove here as well
using UnityEngine.PlayerLoop;

public class PaddleLife : MonoBehaviour
{
	[Tooltip("The max number of lives the paddle has")]
	[SerializeField]
	private int MaxHealth = 5;

	/// <summary>
	/// The current number of lives in the paddle
	/// </summary>
	private int CurrentHealth = 0;

	/// <summary>
	/// bool representing is pong collected to reduce lives and such
	/// </summary>
	private bool IsOrbCollected = true;

	// Events
	public delegate void PaddleHealthUpdatedAction();
	public static event PaddleHealthUpdatedAction OnPaddleHealthUpdated;

	private void Start()
	{
		// Initialize pong stats at the start
		Reinitialize();
	}

	private void OnEnable()
	{
		// Hook the event handlers
		OrbCollision.OnOrbCollected += OrbCollected;
		PongCollision.OnPongPaddleCollision += HandleLogic;
	}

	/// <summary>
	/// Reinitializes the paddle to start state
	/// </summary>
	public void Reinitialize()
	{
		// Make sure the health is back to full
		RefillHealth();

		// Makes sure the first hit isnt considered to reduce lives
		IsOrbCollected = true;
	}

	/// <summary>
	/// Get the current health of the paddle
	/// </summary>
	/// <returns> The current health of the paddle </returns>
	public int GetCurrentHealth()
	{
		return CurrentHealth;
	}

	/// <summary>
	/// Gets the max health of the paddle
	/// </summary>
	/// <returns> The max health of the paddle </returns>
	public int GetMaxHealth()
	{
		return MaxHealth;
	}

	/// <summary>
	/// Refills the current health back to full
	/// </summary>
	public void RefillHealth()
	{
		CurrentHealth = MaxHealth;
		OnPaddleHealthUpdated?.Invoke();
	}

	/// <summary>
	/// Drains the health of the paddle empty
	/// </summary>
	public void DrainHealth()
	{
		CurrentHealth = 0;
		OnPaddleHealthUpdated?.Invoke();
	}

	/// <summary>
	/// Reduces health of the paddle by the given value
	/// </summary>
	/// <param name="health"> The health value to reduce </param>
	public void ReduceHealth(int health)
	{
		// subrtracts the given health from current health
		CurrentHealth -= health;

		// Ensures that the value is between 0 and max health
		CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

		OnPaddleHealthUpdated?.Invoke();
	}

	/// <summary>
	/// Handles the logic when a pong and a paddle collides
	/// </summary>
	private void HandleLogic()
	{
		// If the orb was collected in the previous cycle, the paddle health will be refilled
		if(IsOrbCollected)
		{
			RefillHealth();
		}
		// Else, the paddles health is reduced by 1
		else
		{
			ReduceHealth(1);
		}

		// Make sure that orb is collected flag is set to false 
		// either way to handle the next cycle right
		IsOrbCollected = false;
	}

	/// <summary>
	/// Ensures that an orb has been collected in this cycle
	/// </summary>
	private bool OrbCollected()
	{
		IsOrbCollected = true;
		return true; // Redundant (Todo: See if this can be changed by rearranging the way events are handled)
	}
}

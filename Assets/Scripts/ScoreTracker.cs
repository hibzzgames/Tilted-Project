using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
	[Tooltip("Reference to the score text")]
	[SerializeField]
	private TMP_Text scoreText = null;

	/// <summary>
	/// Current Score of the round
	/// </summary>
	private int currentScore = 0;

	private void OnEnable()
	{
		OrbCollision.OnOrbCollected += IncrementScoreByOrbValue;
	}

	private void OnDisable()
	{
		OrbCollision.OnOrbCollected -= IncrementScoreByOrbValue;
	}

	/// <summary>
	/// Increases score by current default value
	/// </summary>
	/// <returns> Returns true if successful </returns>
	bool IncrementScoreByOrbValue()
	{
		incrementScore(1);
		return true;
	}

	/// <summary>
	/// Increases the score by the given points
	/// </summary>
	/// <param name="points"></param>
	public void incrementScore(int points)
	{
		currentScore += points;
		scoreText.text = currentScore.ToString();
	}
}

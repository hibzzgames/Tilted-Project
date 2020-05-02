using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
	[Tooltip("The prefab of the orb")]
	[SerializeField]
	private GameObject OrbPrefab = null;

	/// <summary>
	/// A list of instanced orbs
	/// </summary>
	private List<GameObject> instancedOrbs = new List<GameObject>();

	/// <summary>
	/// Current number of orbs on screen
	/// </summary>	
	private int activeOrbs = 0;

	/// <summary>
	/// Min number of active orbs on screen
	/// </summary>
	private int minActiveOrbs = 1;

	/// <summary>
	/// A reference to the current theme's orb asset
	/// </summary>
	private GameObject OrbAsset = null;

	private void OnEnable()
	{
		OrbCollision.OnOrbCollectedGameObjectReference += RequestDestroyOrb;
		OrbCollision.OnOrbCollected += RequestRepurposeOrb;
	}

	private void OnDisable()
	{
		OrbCollision.OnOrbCollectedGameObjectReference -= RequestDestroyOrb;
		OrbCollision.OnOrbCollected -= RequestRepurposeOrb;
	}

	/// <summary>
	/// Make a request for the orb to be repurposed 
	/// </summary>
	/// <returns> Was the repurpose request successful or not </returns>
	private bool RequestRepurposeOrb()
	{
		// Loop through the instanced orbs list
		foreach(GameObject orb in instancedOrbs)
		{
			// If the orb is disabled
			if(!orb.activeSelf)
			{
				// enable the orb and increase active orb count
				orb.SetActive(true);
				activeOrbs++;

				// Drain the health of repurposed orb to 0
				orb.GetComponent<OrbHealth>().DrainHealth();

				// Returns true indicating that the orb has been repurposed
				return true;
			}
		}

		// if the orb list is empty, or no inactive orb is found, no orb can be repurposed
		return false;
	}

	/// <summary>
	/// This function is called to handle orb collision
	/// </summary>
	private void HandleOrbCollision()
	{
		// If the number of active orbs is less than minimum number of active orbs,
		// then the orb can be repurposed
		if (activeOrbs < minActiveOrbs)
		{
			// Attempt to repurpose available orbs
			RequestRepurposeOrb();
		}
	}

	/// <summary>
	/// Request a new orb on screen
	/// </summary>
	public void RequestNewOrb()
	{
		// check if the orb can be repurposed first
		bool repurposeRequestStatus = RequestRepurposeOrb();

		// if the orb cannot be repurposed, a new orb needs to be added to the list
		if(!repurposeRequestStatus)
		{
			// Create a new orb and add it to the list
			GameObject newOrb = Instantiate(OrbPrefab);
			Instantiate(OrbAsset, newOrb.transform);
			instancedOrbs.Add(newOrb);

			// increment active orb count
			activeOrbs++;
		}
	}

	/// <summary>
	/// Request an orb to be destroyed
	/// </summary>
	/// <param name="OrbToDestroy"> The orb requested to be destroyed </param>
	/// <returns> Was the request to destory sucessful or not</returns>
	private bool RequestDestroyOrb(GameObject OrbToDestroy)
	{
		// Loop through the instanced orbs to check if the orb to be 
		// destroyed exists in the instanced list
		foreach(GameObject orb in instancedOrbs)
		{
			// if the orb exists in the list
			if(orb.GetInstanceID() == OrbToDestroy.GetInstanceID())
			{
				// If the orb was active
				if(orb.activeSelf)
				{
					// disable the orb and reduce the active orb count
					orb.SetActive(false);
					activeOrbs--;
					return true;
				}

				// If the orb requested to be destroyed is already disabled, 
				// it cannot be destroyed 
				return false;
			}
		}

		// Requested orb was not found and cannot be destroyed
		return false;
	}

	/// <summary>
	/// Adds a child to the orb asset
	/// </summary>
	/// <param name="childAsset"> The asset added as child to the orb </param>
	public void AddOrbAsset(GameObject childAsset)
	{
		// Sets the given child asset as the orb asset, so it can be 
		// instantiated as child when the prefab gets instantiated
		OrbAsset = childAsset;
	}
}

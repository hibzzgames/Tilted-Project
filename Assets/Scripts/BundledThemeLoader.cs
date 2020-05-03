using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// TODO: Comment this file

public class BundledThemeLoader : MonoBehaviour
{
	[Header("Theme Properties")]
	[Tooltip("The name of the theme to load as an asset bundle")]
	[InspectorName("Theme Name")]
	public string bundleName = "prototypetheme";

	[Header("Object Properties")]
	public string DependencyManagerName = "ThemeDependeciesManager";
	public string PongAssetName = "PongAsset";
	public string PaddleAssetName = "PaddleAsset";
	public string BackgroundAssetName = "BackgroundAsset";
	public string OrbAssetName = "OrbAsset";

	[Header("Gameobject References")]
	public GameObject Pong = null;
	public GameObject Paddle = null;
	public GameObject Background = null;
	public OrbManager orbManager = null;

	private ThemeDependencies dependencies;

	private IEnumerator Start()
	{
		// The name of the bundle is loaded from the static information file
		bundleName = StaticInformation.ThemeName;

#if DEBUG
		// If the theme name is empty, set theme default to light theme
		if(bundleName.Equals("")) { bundleName = "lighttheme"; }
#else
		if(bundleName.Equals("")) { bundleName = "lighttheme"; }
#endif

		// A requse to load the asset bundle is made
		AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(
			Path.Combine(Application.streamingAssetsPath, bundleName));

		yield return asyncBundleRequest;

		// Get the loaded asset bundle
		AssetBundle localAssetBundle = asyncBundleRequest.assetBundle;

		// Throw an error when the loaded asset bundle is empty
		if(localAssetBundle == null)
		{
			Debug.LogError("Failed to load asset bundle of type " + bundleName);
			yield break;
		}

		// initialize asset request variable
		AssetBundleRequest assetRequest;

		// Load required dependencies
		assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(DependencyManagerName);
		yield return assetRequest;

		// Load the dependencies for other to access and prepare the data
		GameObject dependencyManagerObject = assetRequest.asset as GameObject;
		dependencies = dependencyManagerObject.GetComponent<ThemeDependencies>();
		dependencies.PrepareAsset();

		// Loads Pong asset and instantiates that as a child
		assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(PongAssetName);
		yield return assetRequest;

		GameObject PongAsset = assetRequest.asset as GameObject;
		CheckDependecies(PongAsset, ThemeDependencies.DepedencyTags.Pong);
		Instantiate(PongAsset, Pong.transform);

		// Loads Paddle asset and instantiates that as a child
		assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(PaddleAssetName);
		yield return assetRequest;

		GameObject PaddleAsset = assetRequest.asset as GameObject;
		CheckDependecies(PongAsset, ThemeDependencies.DepedencyTags.Paddle);
		Instantiate(PaddleAsset, Paddle.transform);

		// Loads background asset and instantiates that as a child
		assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(BackgroundAssetName);
		yield return assetRequest;

		GameObject BackgroundAsset = assetRequest.asset as GameObject;
		CheckDependecies(BackgroundAsset, ThemeDependencies.DepedencyTags.Background);
		Instantiate(BackgroundAsset, Background.transform);

		// Load orb asset and request orbmanager to add the asset as a child of orb
		assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(OrbAssetName);
		yield return assetRequest;

		GameObject OrbAsset = assetRequest.asset as GameObject;
		CheckDependecies(OrbAsset, ThemeDependencies.DepedencyTags.Orb);
		orbManager.AddOrbAsset(OrbAsset);

		// Unload asset bundle (keeping the loaded assets)
		localAssetBundle.Unload(false);

		// Todo: replace these with theme has loaded event
		orbManager.RequestNewOrb();
	}

	/// <summary>
	/// Private function used to check and apply dependecies and overrides
	/// </summary>
	/// <param name="gameObject"> A reference to the gameobject </param>
	/// <param name="tag"> Tag of the object </param>
	private void CheckDependecies(GameObject gameObject, ThemeDependencies.DepedencyTags tag)
	{
		if(dependencies.shaderDependecies.ContainsKey(tag))
		{
			ShaderLoader.Load(gameObject, dependencies.shaderDependecies[tag]);
		}
	}
}

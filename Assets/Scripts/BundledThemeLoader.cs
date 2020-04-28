using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BundledThemeLoader : MonoBehaviour
{
	[Header("Theme Properties")]
	[Tooltip("The name of the theme to load as an asset bundle")]
	[InspectorName("Theme Name")]
	public string bundleName = "prototypetheme";

	[Header("Object Properties")]
	public string PongAssetName = "PongAsset";
	public string PaddleAssetName = "PaddleAsset";
	public string BackgroundAssetName = "BackgroundAsset";

	[Header("Gameobject References")]
	public GameObject Pong = null;
	public GameObject Paddle = null;
	public GameObject Background = null;

	private IEnumerator Start()
	{
		bundleName = StaticInformation.ThemeName;

		// If the theme name is empty, set theme default to light theme
		if(bundleName.Equals("")) { bundleName = "lighttheme"; }

		AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(
			Path.Combine(Application.streamingAssetsPath, bundleName));

		yield return asyncBundleRequest;

		AssetBundle localAssetBundle = asyncBundleRequest.assetBundle;

		if(localAssetBundle == null)
		{
			Debug.LogError("Failed to load asset bundle of type " + bundleName);
		}

		AssetBundleRequest assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(PongAssetName);
		yield return assetRequest;

		GameObject PongAsset = assetRequest.asset as GameObject;
		Instantiate(PongAsset, Pong.transform);

		assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(PaddleAssetName);
		yield return assetRequest;

		GameObject PaddleAsset = assetRequest.asset as GameObject;
		Instantiate(PaddleAsset, Paddle.transform);

		assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(BackgroundAssetName);
		yield return assetRequest;

		GameObject BackgroundAsset = assetRequest.asset as GameObject;
		Instantiate(BackgroundAsset, Background.transform);

		localAssetBundle.Unload(false);
	}
}

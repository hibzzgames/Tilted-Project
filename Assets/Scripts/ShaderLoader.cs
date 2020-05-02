using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderLoader : MonoBehaviour
{
	public static void Load(GameObject objectReference, string ShaderName)
	{
		Material material = new Material(Shader.Find(ShaderName));
		objectReference.GetComponent<Renderer>().material = material;
	}
}

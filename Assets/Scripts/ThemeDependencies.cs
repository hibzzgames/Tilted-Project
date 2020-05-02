using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeDependencies : MonoBehaviour
{
    [Tooltip("A list of shader dependency pairs")]
    public List<ShaderDependecy> shaderDependeciesList;

    [HideInInspector]
    public Dictionary<DepedencyTags, string> shaderDependecies;

    public void PrepareAsset()
    {
        shaderDependecies = new Dictionary<DepedencyTags, string>();

        // move the value from list to dictionary
        foreach(ShaderDependecy dependecy in shaderDependeciesList)
        {
            shaderDependecies.Add(dependecy.tag, dependecy.ShaderReference);
        }
    }

    [Serializable]
    public struct ShaderDependecy
    {
        public DepedencyTags tag;
        public string ShaderReference;
    }

    /// <summary>
    /// List of available theme-based objects
    /// </summary>
    public enum DepedencyTags
    {
        Pong,
        Paddle,
        Background,
        Orb
    }
}

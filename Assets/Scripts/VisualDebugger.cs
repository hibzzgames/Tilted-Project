using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualDebugger : MonoBehaviour
{
    [Tooltip("Text area of the debug text")]
    [SerializeField]
    private TextMeshProUGUI DebugTextMesh = null;

    /// <summary>
    /// Adds the given text to the end of the current log
    /// </summary>
    /// <param name="text"> The text to be added </param>
    public void Log(string text)
    {
        DebugTextMesh.text = DebugTextMesh.text + "\n[" + Time.time + "] " + text;
    }

    /// <summary>
    /// Clears the current log
    /// </summary>
    public void Clear()
    {
        DebugTextMesh.text = "";
    }
}

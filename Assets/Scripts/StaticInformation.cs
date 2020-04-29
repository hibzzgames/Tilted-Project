using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInformation : MonoBehaviour
{
    /// <summary>
    /// Name of the current theme to use during gameplay
    /// </summary>
    public static string ThemeName = "";

    #region screen related properties

    public static float ScreenWidth = Screen.width;
    public static float ScreenHeight = Screen.height;

	#endregion
}

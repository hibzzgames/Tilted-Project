using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ThemeSelectionScript : MonoBehaviour
{
    public TMP_Dropdown themeDropdown;
    public void LoadGame()
    {
        string value = themeDropdown.options[themeDropdown.value].text;

        if(value.Equals("Prototype"))
        {
            StaticInformation.ThemeName = "prototypetheme";
        }
        else if(value.Equals("Light"))
        {
            StaticInformation.ThemeName = "lighttheme";
        }

        SceneManager.LoadScene("PrototypeScene");
    }
}

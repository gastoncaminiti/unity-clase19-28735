using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnEndInputPlayerName(string newString)
    {
        ProfileManager.instance.SetPlayerName(newString);
    }

    public void OnChangeSliderMouse(float newValue)
    {
        ProfileManager.instance.SetMouseSensitivity(newValue);
    }

    public void OnChangeToggleShowName(bool newStatus)
    {
        ProfileManager.instance.SetVisibleName(newStatus);
    }
}

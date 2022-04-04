using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("HumanPlayerScene");
    }

    public void OpenYoutubeLink()
    {
        string link = "https://youtu.be/8oEu9s21-XQ";
        Application.OpenURL(link);
    }
}

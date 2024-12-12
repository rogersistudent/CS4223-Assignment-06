using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public InputField username;

    public void StartGame()
    {
        PlayerPrefs.SetInt("lives", 3);
        PlayerPrefs.SetString("username", username.text);
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

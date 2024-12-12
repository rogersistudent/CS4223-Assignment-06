using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ExitHandler : MonoBehaviour
{

    void Start()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        EditorApplication.isPlaying = false;
    }
}

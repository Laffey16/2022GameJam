using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Playground");
    }

    /// <summary>
    /// We're not doing a tutorial...
    /// </summary>
    public void Tutorial()
    {
        Debug.Log("It's okay you don't need a tutorial.");
    }

    /// <summary>
    /// Goes back idk what to tell you.
    /// </summary>
    public void BackToMain()
    {
        SceneManager.LoadScene("Menu");
    }


    /// <summary>
    /// Quits to desktop.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
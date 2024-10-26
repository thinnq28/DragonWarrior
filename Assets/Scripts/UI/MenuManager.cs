using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;

    //PLay game
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    //Activate game over screen
    public void Help()
    {
        System.Diagnostics.Process.Start("https://web.facebook.com/thinnqfpt");
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
}

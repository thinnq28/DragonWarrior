using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;


    [Header("Game Win")]
    [SerializeField] private GameObject gameWinScreen;
    [SerializeField] private AudioClip gameWinSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        gameWinScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Nếu màn hình đã pause thì bỏ qua và ngược lại là kích hoạt
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //Restart level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
    #endregion

    #region Win game
    //Win game function
    public void GameWin()
    {
        gameWinScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameWinSound);
    }

    //Restart level
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    //Activate game win screen
    public void MainMenuWin()
    {
        SceneManager.LoadScene(0);
    }

    //Quit game/exit play mode if in Editor
    public void QuitGame()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //If status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 (time stops)
        //when it's false change it back to 1 (time goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        //mỗi lần tăng hoặc giảm sẽ là 20%
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}

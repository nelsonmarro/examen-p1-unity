using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;
    public GameObject LossMenuUI;

    public void ShowWinMenu()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Pausar el juego
    }
    public void ShowLossMenu()
    {
        LossMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Pausar el juego
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Reanudar el tiempo de juego
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Detener el tiempo de juego
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;  // Reanudar el tiempo para cargar el menú
        SceneManager.LoadScene("MainMenu");  // Cambia esta línea si el nombre de tu menú principal es diferente
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");  // Esto no se mostrará en la versión compilada
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Reanudar el tiempo para reiniciar
        GameManager.Instance.coins = 0;  // Reinicia el estado del juego
        UIManager.Instance?.UpdateCoinCount(0); // Actualiza el contador en la UI
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual
    }
}

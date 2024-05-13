using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Llama a esta función para salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");  // Solo verás esto en el editor de Unity
    }
}

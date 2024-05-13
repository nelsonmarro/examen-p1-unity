using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int coins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Opcional, dependiendo si quieres que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

        ResetGame();  // Asegura reiniciar el estado del juego cada vez que se instancie o recargue
    }

    public void ResetGame()
    {
        coins = 0;  // Reinicia el conteo de monedas
        UIManager.Instance?.UpdateCoinCount(coins); // Actualiza el contador en la UI
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UIManager.Instance?.UpdateCoinCount(coins);
    }
}

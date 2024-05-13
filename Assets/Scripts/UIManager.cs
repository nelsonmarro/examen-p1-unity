using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;  // Singleton instance

    public TextMeshProUGUI coinText;  // Referencia al texto de UI para monedas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Opcional, para persistir entre cargas de escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCoinCount(int coins)
    {
        coinText.text = coins.ToString();  // Actualizar el texto de UI
    }
}

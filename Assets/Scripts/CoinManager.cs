using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Asegúrate de que tu jugador tiene el tag "Player"
        {
            GameManager.Instance.AddCoins(1);  // Sumar moneda al total
            Destroy(gameObject);  // Destruir la moneda
        }
    }
}

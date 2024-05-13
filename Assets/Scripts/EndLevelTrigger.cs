using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Asegúrate de que tu jugador tiene el tag "Player"
        {
            FindObjectOfType<PauseMenu>().ShowWinMenu();
            gameObject.SetActive(false);
        }
    }
}

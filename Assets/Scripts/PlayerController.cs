using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed = 5f;    // Velocidad de movimiento del jugador
    public float jumpForce = 10f;   // Fuerza de salto del jugador
    private Rigidbody rb;
    private bool isGrounded;
    public LayerMask groundLayer;   // Asegúrate de configurar esto en el inspector de Unity para incluir solo las capas que son "suelo"
    public float groundCheckDistance = 0.2f;  // Distancia del raycast para verificar el suelo
    private int jumpCount = 0;  // Contador de saltos
    private int maxJumpCount = 2;  // Máximo número de saltos permitidos
    private Renderer _renderer;
    public float flashDuration = 0.5f;  // Duración del destello en segundos

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        CheckFall();

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCount++;  // Incrementar el contador de saltos cada vez que se salta
        }
    }

    void CheckFall()
    {
        // Si el jugador cae por debajo de un cierto nivel Y, restablece su posición
        if (transform.position.y < -10)  // Ajusta -10 al nivel deseado de "caída"
        {
            RestartGame();
        }
    }

    void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();  // Actualizar estado isGrounded con raycasting
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, 0f).normalized;
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * rb.mass);
        }

        // Clamping the vertical speed
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -10f, 10f), rb.velocity.z);
    }

    bool CheckIfGrounded()
    {
        // Usar Raycast para comprobar si está en el suelo
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            return hit.normal.y > 0.7; // Asegura que la normal es mayormente vertical
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 knockbackDirection = (transform.position - collision.contacts[0].point).normalized;
            Knockback(collision);  // Asegúrate de ajustar la fuerza según la necesidad del gameplay
            FlashPlayer();
            Invoke(nameof(ResetGame), 0.5f);  // Reinicia el juego después de 1 segundo
        }

        // Verificación de la normal para confirmar que es suelo
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.7)  // Asegúrate de que realmente sea suelo
            {
                isGrounded = true;
                jumpCount = 0;  // Resetea el contador de saltos cuando el jugador toca el suelo
            }
        }
    }

    void Knockback(Collision collision)
    {
        Vector3 direction = (collision.transform.position - transform.position).normalized;
        float force = 10f;  // Ajusta este valor según sea necesario
        collision.rigidbody.AddForce(-direction * force, ForceMode.VelocityChange);
    }

    void FlashPlayer()
    {
        StartCoroutine(FlashEffect());
    }

    IEnumerator FlashEffect()
    {
        Color originalColor = _renderer.material.color;
        _renderer.material.color = Color.red;  // Cambia a rojo para el destello
        yield return new WaitForSeconds(flashDuration / 2);
        _renderer.material.color = originalColor;  // Vuelve al color original
    }

    void RestartGame()
    {
        GameManager.Instance.coins = 0;  // Reinicia el estado del juego
        UIManager.Instance?.UpdateCoinCount(0); // Actualiza el contador en la UI
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual
    }

    void ResetGame()
    {
        FindObjectOfType<PauseMenu>().ShowLossMenu();
    }

    void OnDrawGizmos()
    {
        // Dibuja el raycast en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}

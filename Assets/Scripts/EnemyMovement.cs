using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f; // Velocidad de movimiento del enemigo
    public float movementDistance = 5.0f; // Distancia máxima de movimiento a la izquierda o derecha desde el punto inicial

    private Vector3 startPosition;
    private Vector3 leftBoundary;
    private Vector3 rightBoundary;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
        leftBoundary = new Vector3(startPosition.x - movementDistance, startPosition.y, startPosition.z);
        rightBoundary = new Vector3(startPosition.x + movementDistance, startPosition.y, startPosition.z);
    }

    void Update()
    {
        // Mover el enemigo de izquierda a derecha entre los límites establecidos
        if (movingRight)
        {
            if (transform.position.x >= rightBoundary.x)
                movingRight = false;
            else
                transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            if (transform.position.x <= leftBoundary.x)
                movingRight = true;
            else
                transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}

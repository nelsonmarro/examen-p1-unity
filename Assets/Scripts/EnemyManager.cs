using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Arrastra aquí tu prefab del cubo enemigo en el Inspector
    public Vector3[] spawnPositions; // Define las posiciones de spawn en el Inspector

    void Start()
    {
        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }
}


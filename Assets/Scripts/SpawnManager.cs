using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;

    private float zSpawnLimitTop = 235;
    private float zSpawnLimitBottom = 210;
    private float xSpawnLimitLeft = 220;
    private float xSpawnLimitRight = 290;

    public void SpawnEnemies(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float xPosition = Random.Range(xSpawnLimitLeft, xSpawnLimitRight);
            float zPosition = Random.Range(zSpawnLimitBottom, zSpawnLimitTop);

            Vector3 position = new Vector3(xPosition, 1.38f, zPosition);

            GameObject enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation);
            enemy.GetComponent<Enemy>().player = player;

            Health enemyHealth = enemy.GetComponent<Health>();

            gameManager.RegisterEnemy(enemyHealth);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private Health playerHealth;
    [SerializeField] private GameObject gameOverPanel;
    private int aliveEnemies;
    private int round;
    private bool isGameOver = false;

    public event Action<int> OnRoundChanged;
    public event Action<int> OnAliveEnemiesChanged;
    public static event Action OnGameOver;

    void Start()
    {
        playerHealth.OnDeath += GameOver;

        aliveEnemies = 0;
        NextRound();
    }

    public void RegisterEnemy(Health health)
    {
        health.OnDeath += EnemyDied;
        aliveEnemies++;
        OnAliveEnemiesChanged(aliveEnemies);
    }

    private void EnemyDied()
    {
        aliveEnemies--;
        OnAliveEnemiesChanged(aliveEnemies);

        if (isGameOver) return;

        if (aliveEnemies == 0)
        {
            NextRound();
        }
    }

    private void NextRound()
    {
        round++;
        Debug.Log("Round: " + round);
        spawnManager.SpawnEnemies(round);
        OnRoundChanged?.Invoke(round);
    }

    private void GameOver()
    {
        isGameOver = true;
        OnGameOver.Invoke();

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


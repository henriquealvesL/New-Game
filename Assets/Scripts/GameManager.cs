using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    private int aliveEnemies;
    private int round;

    public event Action<int> OnRoundChanged;
    public event Action<int> OnAliveEnemiesChanged;

    void Start()
    {
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


}

using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentRound;
    [SerializeField] TextMeshProUGUI enemiesAlive;
    [SerializeField] GameManager gameManager;

    void OnEnable()
    {
        gameManager.OnRoundChanged += RoundChanged;
        gameManager.OnAliveEnemiesChanged += EnemiesAliveChanged;
    }

    void OnDisable()
    {
        gameManager.OnRoundChanged -= RoundChanged;
        gameManager.OnAliveEnemiesChanged -= EnemiesAliveChanged;
    }

    private void RoundChanged(int round)
    {
        currentRound.text = "Round: " + round;
    }

    private void EnemiesAliveChanged(int enemies)
    {
        enemiesAlive.text = "Enemies Alive: " + enemies;
    }

}

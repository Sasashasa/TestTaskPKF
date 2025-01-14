using UnityEngine;

public class NetworkAdapter : MonoBehaviour
{
    [SerializeField] private BattleController _battleController;
    
    public void SendPlayerAction(int abilityIndex)
    {
        Debug.Log($"[Client] Игрок выбрал способность с индексом {abilityIndex}");
        
        SimulateServerResponseForPlayerAction(abilityIndex);
    }

    public void RequestEnemyAction()
    {
        SimulateServerResponseForEnemyAction();
    }
    
    private void SimulateServerResponseForPlayerAction(int playerAbilityIndex)
    {
        _battleController.PlayerUseAbility(playerAbilityIndex);
        
        NotifyClientEndTurn();
    }
    
    private void SimulateServerResponseForEnemyAction()
    {
        int enemyAbilityIndex = Random.Range(0, _battleController.EnemyAbilities.Count);
        
        while (!_battleController.EnemyAbilities[enemyAbilityIndex].IsReady)
        {
            enemyAbilityIndex = Random.Range(0, _battleController.EnemyAbilities.Count);
        }
        
        Debug.Log($"[Server] Противник выбрал способность с индексом {enemyAbilityIndex}");
        
        _battleController.EnemyUseAbility(enemyAbilityIndex);
        
        NotifyClientEndTurn();
    }
    
    private void NotifyClientEndTurn()
    {
        Debug.Log("[Server] Ход завершён. Клиент уведомлён.");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    public event EventHandler OnChangeUnitStats;
    
    public List<Ability> PlayerAbilities { get; private set; }
    public List<Ability> EnemyAbilities { get; private set; }

    public Unit PlayerUnit => _playerUnit;
    public Unit EnemyUnit => _enemyUnit;

    [SerializeField] private Unit _playerUnit;
    [SerializeField] private Unit _enemyUnit;
    [SerializeField] private BattleUI _battleUI;
    [SerializeField] private NetworkAdapter _networkAdapter;

    private bool _isPlayerTurn;

    private void Start()
    {
        InitializeAbilities();
        StartNewGame();
    }
    
    private void InitializeAbilities()
    {
        PlayerAbilities = new List<Ability>
        {
            new AttackAbility(),
            new BarrierAbility(),
            new CleanseAbility(),
            new FireballAbility(),
            new RegenAbility(),
        };

        EnemyAbilities = new List<Ability>
        {
            new AttackAbility(),
            new BarrierAbility(),
            new CleanseAbility(),
            new FireballAbility(),
            new RegenAbility(),
        };
    }
    
    public void StartNewGame()
    {
        _playerUnit.ResetCurrentHealth();
        _playerUnit.RemoveBarrier();
        _playerUnit.ActiveEffects.Clear();

        _enemyUnit.ResetCurrentHealth();
        _enemyUnit.RemoveBarrier();
        _enemyUnit.ActiveEffects.Clear();

        foreach (Ability ability in PlayerAbilities)
        {
            ability.ResetCooldown();
        }

        foreach (Ability ability in EnemyAbilities)
        {
            ability.ResetCooldown();
        }

        _isPlayerTurn = true;
        
        _battleUI.ActivateAbilityButtons();
        
        OnChangeUnitStats?.Invoke(this, EventArgs.Empty);
        
        Debug.Log("Новая игра началась!");
    }
    
    public void PlayerUseAbility(int abilityIndex)
    {
        Ability selectedAbility = PlayerAbilities[abilityIndex];

        selectedAbility.Use(_playerUnit, _enemyUnit);
        
        _battleUI.DeactivateAbilityButtons();
        
        OnChangeUnitStats?.Invoke(this, EventArgs.Empty);
        
        StartCoroutine(EndTurn());;
    }
    
    public void EnemyUseAbility(int enemyAbilityIndex)
    {
        EnemyAbilities[enemyAbilityIndex].Use(_enemyUnit, _playerUnit);
        
        OnChangeUnitStats?.Invoke(this, EventArgs.Empty);
        
        StartCoroutine(EndTurn());
    }
    
    private IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1f);

        if (_isPlayerTurn)
        {
            _playerUnit.UpdateEffects();
            
            foreach (var ability in PlayerAbilities)
            {
                ability.UpdateCooldown();
            }
        }
        else
        {
            _enemyUnit.UpdateEffects();
            
            foreach (var ability in EnemyAbilities)
            {
                ability.UpdateCooldown();
            }
        }
        
        OnChangeUnitStats?.Invoke(this, EventArgs.Empty);
        
        if (_playerUnit.CurrentHealth <= 0 || _enemyUnit.CurrentHealth <= 0)
        {
            EndGame();
            yield break;
        }
        
        yield return new WaitForSeconds(1f);
        
        _isPlayerTurn = !_isPlayerTurn;

        if (!_isPlayerTurn)
        {
            _networkAdapter.RequestEnemyAction();
        }
        else
        {
            _battleUI.ActivateAbilityButtons();
        }
    }
    
    private void EndGame()
    {
        if (_playerUnit.CurrentHealth <= 0)
        {
            Debug.Log("Вы проиграли!");
        }
        else if (_enemyUnit.CurrentHealth <= 0)
        {
            Debug.Log("Вы выиграли!");
        }
        
        StartNewGame();
    }
}
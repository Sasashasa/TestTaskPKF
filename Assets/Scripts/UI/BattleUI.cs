using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private Image _playerHealthBar;
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private Transform _playerEffectsContainer;
    
    [SerializeField] private Image _enemyHealthBar;
    [SerializeField] private TextMeshProUGUI _enemyHealthText;
    [SerializeField] private Transform _enemyEffectsContainer;
    
    [SerializeField] private Button[] _abilityButtons;
    [SerializeField] private TextMeshProUGUI[] _abilityCooldownTexts;

    [SerializeField] private Button _restartButton;

    [SerializeField] private BattleController _battleController;
    [SerializeField] private NetworkAdapter _networkAdapter;

    [SerializeField] private EffectSingleUI _iconPrefab;

    private void Awake()
    {
        _restartButton.onClick.AddListener(_battleController.StartNewGame);
        
        for (int i = 0; i < _abilityButtons.Length; i++)
        {
            int index = i;
            
            _abilityButtons[i].onClick.AddListener(() => _networkAdapter.SendPlayerAction(index));
        }
        
        _battleController.OnChangeUnitStats += BattleController_OnChangeUnitStats;
    }

    public void ActivateAbilityButtons()
    {
        for (int i = 0; i < _battleController.PlayerAbilities.Count; i++)
        {
            Ability ability = _battleController.PlayerAbilities[i];
            
            _abilityCooldownTexts[i].text = ability.IsReady ? "" : ability.CurrentCooldown.ToString();
            
            _abilityButtons[i].interactable = ability.IsReady;
        }
    }

    public void DeactivateAbilityButtons()
    {
        for (int i = 0; i < _battleController.PlayerAbilities.Count; i++)
        {
            Ability ability = _battleController.PlayerAbilities[i];
            
            _abilityCooldownTexts[i].text = ability.IsReady ? "" : ability.CurrentCooldown.ToString();
            
            _abilityButtons[i].interactable = false;
        }
    }

    private void BattleController_OnChangeUnitStats(object sender, EventArgs e)
    {
        UpdateHealthBars();
        UpdateUnitEffects(_battleController.PlayerUnit, _playerEffectsContainer);
        UpdateUnitEffects(_battleController.EnemyUnit, _enemyEffectsContainer);
    }
    
    private void UpdateHealthBars()
    {
        _playerHealthBar.fillAmount = (float)_battleController.PlayerUnit.CurrentHealth / _battleController.PlayerUnit.MaxHealth;
        _playerHealthText.text = $"{_battleController.PlayerUnit.CurrentHealth}/{_battleController.PlayerUnit.MaxHealth}";

        _enemyHealthBar.fillAmount = (float)_battleController.EnemyUnit.CurrentHealth / _battleController.EnemyUnit.MaxHealth;
        _enemyHealthText.text = $"{_battleController.EnemyUnit.CurrentHealth}/{_battleController.EnemyUnit.MaxHealth}";
    }

    private void UpdateUnitEffects(Unit unit, Transform effectsContainer)
    {
        foreach (Transform child in effectsContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var effect in unit.ActiveEffects)
        {
            EffectSingleUI effectIcon = Instantiate(_iconPrefab, effectsContainer);
            effectIcon.SetImage(effect);
            effectIcon.SetDurationText(effect.Duration);
        }
    }
}
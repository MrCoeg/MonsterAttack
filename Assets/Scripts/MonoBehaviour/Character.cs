using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common.Utility;
using UnityEngine.UIElements;

public class Character : MonoBehaviour

{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterBaseInformation characterBaseStats;

    [SerializeField] private ObservableVariable<int> _health;
    [SerializeField] private ObservableVariable<int> _attack;
    [SerializeField] private ObservableVariable<int> _defense;

    public List<IAbility> Abilities = new List<IAbility>();
    public CharacterBaseInformation CharacterBaseStats => characterBaseStats;



    public ObservableVariable<int> FinalHealth
    {
        get => _health;
    }
    public ObservableVariable<int> FinalAttack => _attack;
    public ObservableVariable<int> FinalDefense => _defense;


    [SerializeField] private Dictionary<string, IBuffAbility> _buffs = new Dictionary<string, IBuffAbility>();
    [SerializeField] private Dictionary<string, IDebuffAbility> _debuffs = new Dictionary<string, IDebuffAbility>();

    public Animator CharacterAnimator => _animator;

    public Dictionary<string, IBuffAbility> Buffs => _buffs;
    public Dictionary<string, IDebuffAbility> Debuffs => _debuffs;

    private void Awake()
    {
        BattleManager.OnBattleStart += Initialize;
        _animator = GetComponent<Animator>();
    }

    public void Initialize()
    {
        _health = new ObservableVariable<int>(characterBaseStats.Health.Value);
        _attack = new ObservableVariable<int>(characterBaseStats.Attack.Value);
        _defense = new ObservableVariable<int>(characterBaseStats.Defense.Value);
        Debug.Log("Character is initialized" + name);
        InitializeAbility();
    }

    public virtual void PerformAnimation(CharacterAbilityEnum animationTrigger)
    {
    }

    public void TakeDamage(int amount)
    {
        BattleManager.Instance.TriggerNotifications(CharacterBaseStats.ProfilePicture, "Damage Taken -> " + amount);
        FinalHealth.Value -= amount;
        OnDamaged();

    }

    public virtual void OnDamaged()
    {
        Debug.Log("Character is on damaged");
    }



    public void AddBuff(IBuffAbility passiveAbility)
    {
        switch (passiveAbility.targetStats)
        {
            case StatsType.Attack:
                _attack.Value += (int)passiveAbility.buffAmount;
                break;
            case StatsType.Defense:
                _defense.Value += (int)passiveAbility.buffAmount;
                break;
            case StatsType.Health:
                _health.Value += (int)passiveAbility.buffAmount;
                break;
        }
        if (_buffs.ContainsKey(passiveAbility.GetName()))
        {
            _buffs[passiveAbility.GetName()].buffTurnCountBeforeExpire = passiveAbility.buffTurnCountBeforeExpire;
        }
        else
        {
            _buffs.Add(passiveAbility.GetName(), passiveAbility);
        }
    }

    public void AddDebuff(IDebuffAbility debuff)
    {
        switch (debuff.targetStats)
        {
            case StatsType.Attack:
                _attack.Value -= (int)debuff.debuffAmount;
                break;
            case StatsType.Defense:
                _defense.Value -= (int)debuff.debuffAmount;
                break;
            case StatsType.Health:
                _health.Value -= (int)debuff.debuffAmount;
                break;
        }
        if (_debuffs.ContainsKey(debuff.GetName()))
        {
            _debuffs[debuff.GetName()].debuffTurnCountBeforeExpire = debuff.debuffTurnCountBeforeExpire;
        }
        else
        {
            _debuffs.Add(debuff.GetName(), debuff);
        }
    }

    public void RemoveBuff(string buffName)
    {
        if (_buffs.ContainsKey(buffName))
        {
            _buffs[buffName].ApplyBuff(this);
        }
    }

    public void RemoveDebuff(string debuffName)
    {
        if (_debuffs.ContainsKey(debuffName))
        {
            _debuffs[debuffName].ApplyDebuff(this);
        }
    }



    public virtual  void InitializeAbility()
    {

    }


    public virtual void PerformAction(CharacterAbilityEnum ability)
    {

    }
}


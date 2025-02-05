using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Utility;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;
using UnityEngine.TextCore.Text;
using static UnityEngine.Rendering.DebugUI;

public class Drafter : MonoBehaviour
{
    [SerializeField] private Character[] _characters;
    [SerializeField] private Character _pickedCharacter;
    [SerializeField] private ObservableVariable<Character> _target;


    public event Action OnStartTurn;
    public event Action OnPreparation;
    public event Action OnAction;
    public event Action OnImpact;
    public event Action OnEndTurn;

    private void Awake()
    {
        _characters = GetComponentsInChildren<Character>();
    }

    public Character[] Characters
    {
        get => _characters; 
        set
        {
            _characters = value;
            if (_characters.Length  == 0)
            {
                BattleManager.Instance.MarkLoseDrafter(this);
            }
        }
    }
    public Character PickedCharacter
    {
        get { return _pickedCharacter; }
        set
        {
            _pickedCharacter = value;
            OnCharacterSelected(_pickedCharacter);
        }
    }
    
    public virtual void OnCharacterSelected(Character pickedCharacter)
    {

    }

    public virtual void StartTurn()
    {
        OnStartTurn?.Invoke();

    }

    public virtual void Preparation()
    {
        OnPreparation?.Invoke();
    }

    public virtual void Action(CharacterAbilityEnum abilityEnum)
    {
        OnAction?.Invoke();
    }

    public virtual void Impact()
    {
        OnImpact?.Invoke();
    }

    public virtual void EndTurn()
    {
        OnEndTurn?.Invoke();
    }
}

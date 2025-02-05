using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBaseStats", menuName = "ScriptableObjects/CharacterBaseStats", order = 1)]
public class CharacterBaseInformation : ScriptableObject
{
    [SerializeField] private ObservableVariable<string> _characterName;
    [SerializeField] private ObservableVariable<int> _health;
    [SerializeField] private ObservableVariable<int> _attack;
    [SerializeField] private ObservableVariable<int> _defense;

    [SerializeField] private ObservableVariable<int> _attackMultiplier;
    [SerializeField] private ObservableVariable<int> _defenseMultiplier;

    [SerializeField] private Sprite _profilePicture;
    [SerializeField] private BattleIconEnum _AttackIcon;
    [SerializeField] private BattleIconEnum _DefenseIcon;
    [SerializeField] private BattleIconEnum _talentIcon;
    [SerializeField] private GameObject _VFXAttack;
    [SerializeField] private GameObject _VFXDefense;
    [SerializeField] private GameObject _VFXPassive;

    public string CharacterName => _characterName.Value;
    public ObservableVariable<int> Health => _health;
    public ObservableVariable<int> Attack => _attack;
    public ObservableVariable<int> Defense => _defense;
    public ObservableVariable<int> AttackMultiplier => _attackMultiplier;
    public ObservableVariable<int> DefenseMultiplier => _defenseMultiplier;
    public Sprite ProfilePicture => _profilePicture;
    public BattleIconEnum AttackIcon => _AttackIcon;
    public BattleIconEnum DefenseIcon => _DefenseIcon;
    public BattleIconEnum TalentIcon => _talentIcon;
    public GameObject VFXAttack => _VFXAttack;
    public GameObject VFXDefense => _VFXDefense;
    public GameObject VFXPassive => _VFXPassive;





}

public enum CharacterStatEnum
{
    [StatFieldName("_characterName")]
    CharacterName,
    [StatFieldName("_health")]
    Health,
    [StatFieldName("_attack")]
    Attack,
    [StatFieldName("_defense")]
    Defense,
    [StatFieldName("_attackMultiplier")]
    AttackMultiplier,
    [StatFieldName("_defenseMultiplier")]
    DefenseMultiplier
}


public interface ICharacterBehaviour 
{
    IAbility AttackAbility { get; }
    IAbility DefendAbility { get; }
    IAbility PassiveAbility { get; }
    void PerformAction(CharacterAbilityEnum ability);
}

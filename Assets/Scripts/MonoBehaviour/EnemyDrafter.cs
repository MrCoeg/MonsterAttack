using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrafter : Drafter
{
    override public void StartTurn()
    {
        base.StartTurn();
        Debug.Log("Enemy Start Turn");
        Preparation();
    }

    override public void Preparation()
    {
        base.Preparation();
        Debug.Log("Enemy Preparation");
        PickedCharacter = Characters[Random.Range(0, Characters.Length)];
        Debug.Log("Enemy Picked Character: " + PickedCharacter.name);
        var nrAbility =Random.Range(0, 20);
        var abilityEnum = nrAbility < 10 ? CharacterAbilityEnum.Attack : CharacterAbilityEnum.Defend;

        Debug.Log("Enemy Picked Ability: " + abilityEnum);
        Action(abilityEnum);

    }

    override public void Action(CharacterAbilityEnum abilityEnum)
    {
        base.Action(abilityEnum);
        Debug.Log("Enemy Action");
        PickedCharacter.PerformAnimation(abilityEnum);
    }

    override public void Impact()
    {
        base.Impact();
        Debug.Log("Enemy Impact");
        EndTurn();
    }

    override public void EndTurn()
    {
        Debug.Log("Enemy End Turn");
        base.EndTurn();
    }
}

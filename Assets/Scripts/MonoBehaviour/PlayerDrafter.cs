using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrafter : Drafter
{
    [SerializeField] private ButtonBinder _buttonBinder;

    override public void OnCharacterSelected(Character pickedCharacter)
    {
        SubscriberManager.BindTextsObservableSubscriber(
            new Dictionary<SubscriberEnum, ObservableVariable<int>>()
            {
                    {SubscriberEnum.HealthText, pickedCharacter.FinalHealth},
                    {SubscriberEnum.AttackText, pickedCharacter.FinalAttack},
                    {SubscriberEnum.DefendText, pickedCharacter.FinalDefense}
            });

        _buttonBinder.BindCharacterAction(pickedCharacter);
    }

    override public void StartTurn()
    {
        _buttonBinder.BindCharacterAction(PickedCharacter);
        base.StartTurn();
    }

    override public void Preparation()
    {
        base.Preparation();
    }

    override public void Action(CharacterAbilityEnum abilityEnum)
    {
        base.Action(abilityEnum);
        PickedCharacter.PerformAnimation(abilityEnum);
    }

    override public void Impact()
    {
        base.Impact();
        EndTurn();
    }

    override public void EndTurn()
    {
        _buttonBinder.UnBindButton();
        base.EndTurn();

    }




}

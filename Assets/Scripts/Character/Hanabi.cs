using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.TextCore.Text;
using static Unity.VisualScripting.Member;

public class Hanabi : Character, ICharacterBehaviour
{
    public IAbility AttackAbility { get; private set; }
    public IAbility DefendAbility { get; private set; }
    public IAbility PassiveAbility { get; private set; }



    public override void PerformAction(CharacterAbilityEnum ability)
    {
        Debug.Log("Hanabi is performing action: " + ability);
        var oppositeDraft = BattleManager.Instance.GetOppositeDrafter();
        var friendDraft = BattleManager.Instance.GetActiveDraft();   
        switch (ability)
        {
            case CharacterAbilityEnum.Attack:
                AttackAbility.Execute(this, oppositeDraft.PickedCharacter);

                var pickedCharacterIndex = Array.IndexOf(oppositeDraft.Characters, oppositeDraft.PickedCharacter);
                var oppositeCharacters = oppositeDraft.Characters;

                Character leftCharacter = (pickedCharacterIndex > 0) ? oppositeCharacters[pickedCharacterIndex - 1] : null;
                Character rightCharacter = (pickedCharacterIndex < oppositeCharacters.Length - 1) ? oppositeCharacters[pickedCharacterIndex + 1] : null;

                if (leftCharacter != null)
                    PassiveAbility.Execute(this, leftCharacter);
                if (rightCharacter != null)
                    PassiveAbility.Execute(this, rightCharacter);

                break;
            case CharacterAbilityEnum.Defend:
                foreach (var t in friendDraft.Characters)
                {
                    DefendAbility.Execute(this, t);
                }
                break;

        }

        BattleManager.Instance.GetActiveDraft().Impact();
    }

    public override void PerformAnimation(CharacterAbilityEnum animationTrigger)
    {
        base.PerformAnimation(animationTrigger);
        StartCoroutine(PerformAnimationAndAction(animationTrigger));

    }

    IEnumerator PerformAnimationAndAction(CharacterAbilityEnum animationTrigger)
    {
        CharacterAnimator.SetTrigger(animationTrigger.ToString());

        while (CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
        {
            yield return null;
        }
        PerformAction(animationTrigger);
    }

    public override void InitializeAbility()
    {
        BattleManager.Instance.GetActiveDraft().OnStartTurn += () =>
        {
            var draft =  BattleManager.Instance.GetActiveDraft();
            foreach (var character in draft.Characters)
            {
                character.RemoveBuff(DefendAbility.GetName());
            }
        };

        Debug.Log("Initializing Hanabi abilities");

        AttackAbility = new HanabiAttackAbility();
        DefendAbility = new HanabiDefendAbility();
        PassiveAbility = new HanabiPassiveAbility();
    }

    public override void OnDamaged()
    {
        base.OnDamaged();
        CharacterAnimator.SetTrigger(CharacterAbilityEnum.Damaged.ToString());
        if (FinalHealth.Value <= 0)
        {
            BattleManager.Instance.RemoveDeadCharacter(this);
            CharacterAnimator.SetTrigger("Dead");
        }
    }
}

public class HanabiAttackAbility : IAbility
{
    public void Execute(Character source, Character target)
    {

        var damage = source.FinalAttack.Value - target.FinalDefense.Value;
        if (damage < 0)
        {
            damage = 0;
        }

        BattleManager.Instance.TriggerNotifications(source.CharacterBaseStats.ProfilePicture, "Skills -> " + GetName() );


        target.TakeDamage(damage);
    }

    public string GetName() => "RoseScar";
}

public class HanabiDefendAbility : IBuffAbility
{
    public int buffTurnCountBeforeExpire { get; set; } = 2;

    public float buffAmount { get; set; }

    public StatsType targetStats { get; set; }

    public void ApplyBuff(Character target)
    {

        buffTurnCountBeforeExpire --;
        if (buffTurnCountBeforeExpire == 0)
        {
            target.Buffs.Remove(GetName());

            target.FinalDefense.Value -= (int)buffAmount;

            var vfx = target.GetComponentInChildren<ParticleSystem>().gameObject;
            MonoBehaviour.Destroy(vfx);
        }

    }

    public void Execute(Character user, Character target)
    {
        BattleManager.Instance.TriggerNotifications(user.CharacterBaseStats.ProfilePicture, "Skills -> " + GetName() + "To" + target.CharacterBaseStats.CharacterName);
        
        if (target.Buffs.ContainsKey(GetName()))
        {
            target.Buffs[GetName()].buffTurnCountBeforeExpire = 2;
            return;
        }


        
        buffAmount = user.FinalDefense.Value * user.CharacterBaseStats.DefenseMultiplier.Value;
        target.FinalDefense.Value += (int)buffAmount;

        GameObject.Instantiate(user.CharacterBaseStats.VFXDefense, target.transform);

        targetStats = StatsType.Defense;
        target.AddBuff(this);

    }

    public string GetName() => "FloweryRock";
}

public class HanabiPassiveAbility : IAbility
{

    public void Execute(Character source, Character target)
    {
        BattleManager.Instance.TriggerNotifications(source.CharacterBaseStats.ProfilePicture, "Passive -> " + GetName() + "To" + target.CharacterBaseStats.CharacterName);
        var damage = (source.FinalAttack.Value - target.FinalDefense.Value) / 3;
        if (damage < 0)
        {
            damage = 0;
        }
        target.TakeDamage(damage);
    }

    public string GetName() => "MoonBlossom";
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Helchurt : Character, ICharacterBehaviour
{
    public IAbility AttackAbility { get; private set; }
    public IAbility DefendAbility { get; private set; }
    public IAbility PassiveAbility { get; private set; }

    public override void PerformAction(CharacterAbilityEnum ability)
    {
        Debug.Log("Helchurt is performing action: " + ability);
        var oppositeDraft = BattleManager.Instance.GetOppositeDrafter();
        var friendDraft = BattleManager.Instance.GetActiveDraft();
        switch (ability)
        {
            case CharacterAbilityEnum.Attack:
                AttackAbility.Execute(this, oppositeDraft.PickedCharacter);
                break;
            case CharacterAbilityEnum.Defend:
                DefendAbility.Execute(this, this);
                break;
            case CharacterAbilityEnum.Passive:
                var character = BattleManager.Instance.GetOppositeDrafter().PickedCharacter;
                PassiveAbility.Execute(this, character);
                break;
        }
        BattleManager.Instance.GetActiveDraft().Impact();
    }

    public override void InitializeAbility()
    {
        Debug.Log("Initializing Helchurt abilities");

        BattleManager.Instance.GetOppositeDrafter().OnStartTurn += () =>
        {
            var draft = BattleManager.Instance.GetActiveDraft();
            foreach (var character in draft.Characters)
            {
                character.RemoveDebuff(PassiveAbility.GetName());
            }
            
        };


        AttackAbility = new HelchurtAttackAbility();
        DefendAbility = new HelchurtDefendAbility();
        PassiveAbility = new HelchurtPassiveAbility();
    }

    public override void OnDamaged()
    {
        base.OnDamaged();
        CharacterAnimator.SetTrigger(CharacterAbilityEnum.Damaged.ToString());
        PassiveAbility.Execute(this, BattleManager.Instance.GetActiveDraft().PickedCharacter);

        if (FinalHealth.Value <= 0)
        {
            BattleManager.Instance.RemoveDeadCharacter(this);
            CharacterAnimator.SetTrigger("Dead");
        }
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



}

public class HelchurtAttackAbility : IAbility
{
    public string GetName()
    {
        return "DarkClaw";
    }

    public void Execute(Character source, Character target)
    {
        BattleManager.Instance.TriggerNotifications(source.CharacterBaseStats.ProfilePicture, "Attacking -> " + target.name);
        var damage = source.FinalAttack.Value - target.FinalDefense.Value;
        if (damage < 0)
        {
            damage = 0;
        }
        target.TakeDamage(damage);
    }
}

public class HelchurtDefendAbility : IBuffAbility
{
    public int buffTurnCountBeforeExpire { get; set; } = 2;
    public float buffAmount { get ; set; }
    public StatsType targetStats { get ; set; }

    public void ApplyBuff(Character target)
    {
        Debug.Log("Applying buff to " + target.name);
        buffTurnCountBeforeExpire--;    

        if (buffTurnCountBeforeExpire == 0)
        {
            target.FinalHealth.Value -= (int)buffAmount;
            target.Buffs.Remove(GetName());
        }
    }

    public void Execute(Character user, Character target)
    {


        BattleManager.Instance.TriggerNotifications(user.CharacterBaseStats.ProfilePicture, "Defending -> " + target.name);

        if (target.Buffs.ContainsKey(GetName()))
        {
            target.Buffs[GetName()].buffTurnCountBeforeExpire = buffTurnCountBeforeExpire;
            return;
        }

        buffAmount = user.FinalHealth.Value / 4;
        targetStats = StatsType.Health; 
        target.FinalHealth.Value += (int)buffAmount;

        user.AddBuff(this);
    }

    public string GetName()
    {
        return "BloodWill";
    }
}

public class HelchurtPassiveAbility : IDebuffAbility
{
    public int debuffTurnCountBeforeExpire { get; set; }
    public float debuffAmount { get; set; }
    public StatsType targetStats { get; set; }

    public void ApplyDebuff(Character target)
    {
        debuffTurnCountBeforeExpire--;

        if (debuffTurnCountBeforeExpire == 0)
        {
            target.Debuffs.Remove(GetName());
            target.FinalAttack.Value += (int)debuffAmount;

            var vfx = target.gameObject.GetComponentInChildren<ParticleSystem>().gameObject;
            MonoBehaviour.Destroy(vfx);
        }
    }

    public void Execute(Character user, Character target)
    {
        if (target.Debuffs.ContainsKey(GetName()))
        {
            target.Debuffs[GetName()].debuffTurnCountBeforeExpire = debuffTurnCountBeforeExpire;
            return;
        }

        Debug.Log(user.name + " is using " + GetName() + " on " + target.name);
        debuffAmount = user.FinalAttack.Value / 4;
        targetStats = StatsType.Attack;
        target.FinalAttack.Value -= (int)debuffAmount;
        target.AddDebuff(this);
        GameObject.Instantiate(user.CharacterBaseStats.VFXPassive, target.transform);
    }

    public string GetName()
    {
        return "BloodCurse";
    }
}


public interface IAbility
{
    void Execute(Character user, Character target);
    string GetName();
}

public interface IBuffAbility : IAbility
{
    public int buffTurnCountBeforeExpire { get; set; }
    float buffAmount { get; set; }
    StatsType targetStats { get; set; }


    void ApplyBuff(Character target);
}

public interface IDebuffAbility : IAbility
{
    public int debuffTurnCountBeforeExpire { get; set; }
    float debuffAmount { get; set; }
    StatsType targetStats { get; set; }
    void ApplyDebuff(Character target);
}


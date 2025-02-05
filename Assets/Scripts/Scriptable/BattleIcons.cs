using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BattleIcons", menuName = "ScriptableObjects/BattleIcons", order = 1)]
public class BattleIcons : ScriptableObject
{
    public Sprite[] icons;
    public string[] iconNames;

    public Sprite GetIcon(BattleIconEnum battleEnum)
    {
        return icons[(int)battleEnum];
    }

}

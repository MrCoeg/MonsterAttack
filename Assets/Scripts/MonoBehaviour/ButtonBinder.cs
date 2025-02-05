using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBinder : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _defendButton;

    public void BindCharacterAction(Character character)
    {

        _attackButton.onClick.RemoveAllListeners();
        _defendButton.onClick.RemoveAllListeners();

        _attackButton.onClick.AddListener(() =>
        {
            BattleManager.Instance.GetActiveDraft().Action(CharacterAbilityEnum.Attack);
        });

        _defendButton.onClick.AddListener(() =>
        {
            BattleManager.Instance.GetActiveDraft().Action(CharacterAbilityEnum.Defend);
        });



        var icons = Resources.Load<BattleIcons>("ScriptableObject/BattleIcons");

        _attackButton.GetComponent<Image>().sprite = icons.GetIcon(
            character.CharacterBaseStats.AttackIcon
            );

        _defendButton.GetComponent<Image>().sprite = icons.GetIcon(
            character.CharacterBaseStats.DefenseIcon
            );

    }

    public void UnBindButton()
    {
        _attackButton.onClick.RemoveAllListeners();
        _defendButton.onClick.RemoveAllListeners();
    }

}

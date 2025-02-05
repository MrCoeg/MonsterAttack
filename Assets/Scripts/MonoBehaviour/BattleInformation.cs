using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using Common.Utility;
using UnityEngine.UI;
using TMPro;
public class BattleInformation : MonoBehaviour
{
    [SerializeField] private GameObject characterInformationPanel;
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Color _buffTextColor = Color.green;
    [SerializeField] private Color _debuffTextColor = Color.red;

    [SerializeField] private List<TextMeshProUGUI> _buffTextPool = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _debuffTextPool = new List<TextMeshProUGUI>();


    private RectTransform _panelRectTransform;
    private void Start()
    {
        characterInformationPanel.SetActive(false);

        _panelRectTransform = characterInformationPanel.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        
            var (isHit, hit) = RaycastUtility.GetGameObject(layerMask);
            if (isHit)
            {
                characterInformationPanel.SetActive(true);
                UpdatePanel(hit.gameObject.GetComponent<Character>());
                _panelRectTransform.position = Input.mousePosition;
            }
            else
            {
                characterInformationPanel.SetActive(false);
            }
        

    }

    private void UpdatePanel(Character character)
    {
        var stats = new int[] { character.FinalHealth.Value, character.FinalAttack.Value, character.FinalDefense.Value };

        var buffs = character.Buffs;
        var debuffs = character.Debuffs;

        var panel = characterInformationPanel.GetComponentsInChildren<VerticalLayoutGroup>();

        var statsBuff = panel[1].GetComponentsInChildren<TextMeshProUGUI>();

        statsBuff[0].text = "Health: " + stats[0];
        statsBuff[1].text = "Attack: " + stats[1];
        statsBuff[2].text = "Defense: " + stats[2];

        ReuseBuffText(panel[2].transform, buffs, _buffTextPool, _buffTextColor);

        ReuseDebuffText(panel[3].transform, debuffs, _debuffTextPool, _debuffTextColor);
    }

    private void ReuseBuffText(Transform parent, Dictionary<string, IBuffAbility> buffs, List<TextMeshProUGUI> textPool, Color textColor)
    {
        int index = 0;

        foreach (var pair in buffs)
        {
            TextMeshProUGUI text;

            if (index < textPool.Count)
            {
                text = textPool[index];
                text.gameObject.SetActive(true);
            }
            else
            {
                text = Instantiate(textPrefab, parent);
                textPool.Add(text);
            }

            text.text = $"{pair.Key} Buff: +{pair.Value.buffAmount} {pair.Value.targetStats}";
            text.color = textColor;

            index++;
        }

        for (int i = index; i < textPool.Count; i++)
        {
            textPool[i].gameObject.SetActive(false);
        }
    }

    private void ReuseDebuffText(Transform parent, Dictionary<string, IDebuffAbility> debuffs, List<TextMeshProUGUI> textPool, Color textColor)
    {
        int index = 0;

        foreach (var pair in debuffs)
        {
            TextMeshProUGUI text;

            if (index < textPool.Count)
            {
                text = textPool[index];
                text.gameObject.SetActive(true);
            }
            else
            {
                text = Instantiate(textPrefab, parent);
                textPool.Add(text);
            }

            text.text = $"{pair.Key} Debuff: -{pair.Value.debuffAmount} {pair.Value.targetStats}";
            text.color = textColor;

            index++;
        }

        for (int i = index; i < textPool.Count; i++)
        {
            textPool[i].gameObject.SetActive(false);
        }
    }







}

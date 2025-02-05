using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraftStatsUI : MonoBehaviour
{
    [SerializeField] private Drafter drafter;
    [SerializeField] private GameObject[] characterPanels;
    [SerializeField] private Image[] healthBars;
    [SerializeField] private Image[] energyBars;
    [SerializeField] private Image[] photoProfiles;


/*    private void Start()
    {
        var characters = drafter.Characters;

        for (int i = 0; i < characters.Length; i++)
        {
            characterPanels[i].SetActive(true);
*//*            healthBars[i].fillAmount = (float)characters[i].FinalHealth.Value / 100;*//*
            photoProfiles[i].sprite = characters[i].CharacterBaseStats.ProfilePicture;
        }
    }*/
}

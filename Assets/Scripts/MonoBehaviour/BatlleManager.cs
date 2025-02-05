using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static event Action OnBattleStart;
    public static event Action OnBattleEnd;
    public static event Action<Character> OnSelectCharacter;

    [SerializeField] private BattlesAction _battlesAction;

    [SerializeField] private Drafter _playerDrafter;
    [SerializeField] private Drafter _enemyDrafter;
    [SerializeField] private Drafter[] _draftersOrder;

    [SerializeField] private int _currentDrafterIndex = 0;
    [SerializeField] private bool _battleActive = true;

    [SerializeField] GameObject winPanel;

    public static BattleManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _playerDrafter.OnEndTurn += EndTurn;
        _enemyDrafter.OnEndTurn += EndTurn;

        _draftersOrder = new Drafter[2] { _playerDrafter, _enemyDrafter };
        /*        _currentDrafterIndex = UnityEngine.Random.Range(0, 2);*/
        _currentDrafterIndex = 0;

    }

    private void Start()
    {
        OnBattleStart?.Invoke();
        _enemyDrafter.PickedCharacter = _enemyDrafter.Characters[0];
        _playerDrafter.PickedCharacter = _playerDrafter.Characters[0];

        _draftersOrder[_currentDrafterIndex].StartTurn();
    }

    public void TriggerNotifications(Sprite activator, string message)
    {
        _battlesAction.Notify(activator, message);
    }



    public void EndTurn()
    {
        Debug.Log("SwitchDraft");
        _currentDrafterIndex = _currentDrafterIndex == 0 ? 1 : 0;
        _draftersOrder[_currentDrafterIndex].StartTurn();
    }

    public void RemoveDeadCharacter(Character character)
    {
        // check if the character is from the player drafter
        if (_playerDrafter.Characters.Contains(character))
        {
            _playerDrafter.Characters = _playerDrafter.Characters.Where(c => c != character).ToArray();
            Destroy(character.gameObject);
        }
        else
        {
            _enemyDrafter.Characters = _enemyDrafter.Characters.Where(c => c != character).ToArray();
            Destroy(character.gameObject);
        }
    }

    public void MarkLoseDrafter(Drafter drafter)
    {
        if (drafter == _playerDrafter)
        {
            Debug.Log("Player Lose");
            _battleActive = false;
            OnBattleEnd?.Invoke();

            // reload the scene
            SceneManager.Instance.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Enemy Lose");
            _battleActive = false;
            OnBattleEnd?.Invoke();
            winPanel.SetActive(true);
        }
    }


    public Drafter GetOppositeDrafter()
    {
        return _draftersOrder[_currentDrafterIndex == 0 ? 1 : 0];
    }

    public Drafter GetActiveDraft()
    {
        return _draftersOrder[_currentDrafterIndex];
    }

}
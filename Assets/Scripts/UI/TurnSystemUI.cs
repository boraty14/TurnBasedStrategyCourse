using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TurnSystemUI : MonoBehaviour
    {
        [SerializeField] private Button _endTurnBtn;
        [SerializeField] private TextMeshProUGUI _turnNumberText;
        [SerializeField] private GameObject _enemyTurnVisualGameObject;

        private void Start()
        {
            _endTurnBtn.onClick.AddListener(CallNextTurn);
            UpdateTurnText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnVisibility();
            TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
        }

        private void OnTurnChanged()
        {
            UpdateTurnText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnVisibility();
        }

        private void CallNextTurn() => TurnSystem.Instance.NextTurn();

        private void UpdateTurnText()
        {
            _turnNumberText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
        }

        private void UpdateEnemyTurnVisual()
        {
            _enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
        }

        private void UpdateEndTurnVisibility()
        {
            _endTurnBtn.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
        }
    }
}

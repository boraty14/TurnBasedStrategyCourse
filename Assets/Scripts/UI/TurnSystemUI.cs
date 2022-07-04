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

        private void Start()
        {
            _endTurnBtn.onClick.AddListener(CallNextTurn);
            UpdateTurnText();
            TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
        }

        private void OnTurnChanged()
        {
            UpdateTurnText();
        }

        private void CallNextTurn() => TurnSystem.Instance.NextTurn();

        private void UpdateTurnText()
        {
            _turnNumberText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectedGameObject;

    private BaseAction _baseAction;
    
    public void SetBaseAction(BaseAction baseAction)
    {
        _baseAction = baseAction;
        _text.text = baseAction.GetActionName().ToUpper();
        
        _button.onClick.AddListener(() => OnActionButtonClick(baseAction));
    }

    private void OnActionButtonClick(BaseAction baseAction)
    {
        UnitActionSystem.Instance.SetSelectedAction(baseAction);
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        _selectedGameObject.SetActive(selectedBaseAction == _baseAction);
    }
}

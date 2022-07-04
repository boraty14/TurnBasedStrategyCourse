using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    private int _turnNumber = 1;
    public Action OnTurnChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one TurnSystem System");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void NextTurn()
    {
        _turnNumber++;
        OnTurnChanged?.Invoke();
    }

    public int GetTurnNumber() => _turnNumber;
}
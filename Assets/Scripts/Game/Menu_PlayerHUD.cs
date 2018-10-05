using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_PlayerHUD : MonoBehaviour {

    [SerializeField] Text smallCollectiblesText;
    [SerializeField] Text largeCollectiblesText;

    void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        GameSession.Instance.OnPlayerDeath += HandlePlayerDeath;
        GameSession.Instance.OnCollect += HandleCollect;
    }
    
    void UnsubscribeToEvents()
    {
        GameSession.Instance.OnPlayerDeath -= HandlePlayerDeath;
        GameSession.Instance.OnCollect -= HandleCollect;
    }

    private void Start()
    {
        RefreshUI();
    }

    void HandlePlayerDeath()
    {
        RefreshUI();
    }

    void HandleCollect()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        smallCollectiblesText.text = GameSession.Instance.CollectiblesSmall.ToString();
        largeCollectiblesText.text = GameSession.Instance.CollectiblesLarge.ToString();
    }
}

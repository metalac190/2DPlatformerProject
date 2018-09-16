using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Stats : MonoBehaviour {

    [SerializeField] int firstLevelIndex = 1;
    [SerializeField] Button tryAgainButton;

    private void Awake()
    {
        FillEmptyReferences();
    }

    void FillEmptyReferences()
    {
        if (tryAgainButton == null)
        {
            Debug.LogError("Menu_Stats: searching for try again button. Fill in Inspector instead");
            tryAgainButton = GetComponentInChildren<Button>();
        }
    }

    private void OnEnable()
    {
        tryAgainButton.onClick.AddListener(StartFirstLevel);
    }

    private void OnDisable()
    {
        tryAgainButton.onClick.RemoveListener(StartFirstLevel);
    }

    public void StartFirstLevel()
    {
        SceneLoader.LoadLevel(firstLevelIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Main : MonoBehaviour {

    [SerializeField] int firstLevelIndex = 1;
    [SerializeField] Button startButton;

    private void Awake()
    {
        FillEmptyReferences();
    }

    void FillEmptyReferences()
    {
        if (startButton == null)
        {
            Debug.LogError("Menu_Main: searching for start button. Fill in Inspector instead");
            startButton = GetComponentInChildren<Button>();
        }
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartFirstLevel);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartFirstLevel);
    }

    public void StartFirstLevel()
    {
        SceneLoader.LoadLevel(firstLevelIndex);
    }
}

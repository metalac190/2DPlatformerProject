using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameSession : MonoBehaviour {

    public static GameSession Instance = null;

    public int PlayerDeaths { get; private set; }
    public int CollectiblesSmall { get; private set; }
    public int CollectiblesLarge { get; private set; }

    public event Action OnPlayerDeath = delegate { };
    public event Action OnCollect = delegate { };

    private void Awake()
    {
        #region Singleton Pattern - Create/Destroy
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGameSession();
            SceneManager.sceneLoaded += HandleNewSceneLoaded;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion
    }

    // this function gets called each time a new scene is loaded
    private void HandleNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    private void InitializeGameSession()
    {
        PlayerDeaths = 0;
        CollectiblesSmall = 0;
        CollectiblesLarge = 0;
    }

    #region Public Methods

    public void ResetGameSession()
    {
        SceneLoader.LoadLevel(0);
        Destroy(gameObject);
    }

    public void AddSmallCollectible()
    {
        CollectiblesSmall += 1;
        OnCollect.Invoke();
    }

    public void AddLargeCollectible()
    {
        CollectiblesLarge += 1;
        OnCollect.Invoke();
    }

    public void ProcessPlayerDeath()
    {
        PlayerDeaths += 1;
        OnPlayerDeath.Invoke();
        SceneLoader.ReloadScene();
    }
    #endregion
}

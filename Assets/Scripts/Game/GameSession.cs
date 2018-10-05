using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameSession : MonoBehaviour {

    #region Singleton Pattern - Lazy Load
    private static GameSession instance = null;
    public static GameSession Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameSession>();
                if(instance == null)
                {
                    GameObject go = new GameObject(typeof(GameSession).ToString());
                    instance = go.AddComponent<GameSession>();
                }
            }

            return instance;
        }
    }
    #endregion

    public int PlayerDeaths { get; private set; }
    public int CollectiblesSmall { get; private set; }
    public int CollectiblesLarge { get; private set; }

    public event Action OnPlayerDeath = delegate { };
    public event Action OnCollect = delegate { };

    private void Awake()
    {
        #region Singleton Pattern - Create/Destroy
        if(Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            InitializeGameSession();
            SceneManager.sceneLoaded += HandleNewSceneLoaded;
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

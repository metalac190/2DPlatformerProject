using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

    #region Singleton Pattern - Lazy Load
    private static ScenePersist instance = null;
    public static ScenePersist Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ScenePersist>();
                if(instance == null)
                {
                    GameObject go = new GameObject(typeof(ScenePersist).ToString());
                    instance = go.AddComponent<ScenePersist>();
                }
            }
            return instance;
        }
    }
    #endregion

    int startingSceneIndex;

    // Use this for initialization
    void Awake ()
    {
        #region Singleton Pattern - Create/Destroy
        if(Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Initialize();
            SceneManager.sceneLoaded += HandleNewSceneLoaded;
        }
        #endregion
    }

    private void HandleNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DestroyIfDifferentLevel();
    }

    private void DestroyIfDifferentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        StoreLevelIndex();
    }

    private void StoreLevelIndex()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}

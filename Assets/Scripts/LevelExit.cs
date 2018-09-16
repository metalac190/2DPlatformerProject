using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelExit : MonoBehaviour {

    [SerializeField] float levelLoadTransitionDelay = 2f;
    [SerializeField] float levelLoadSlowAmount = 0.2f;
    [SerializeField] UnityEvent OnLevelExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        SlowTime(levelLoadSlowAmount);
        yield return new WaitForSecondsRealtime(levelLoadTransitionDelay);
        ReturnTimeToNormalSpeed();
        OnLevelExit.Invoke();
        SceneLoader.LoadNextLevel();
    }

    private static void SlowTime(float newTimeSpeed)
    {
        Time.timeScale = 0.2f;
    }

    void ReturnTimeToNormalSpeed()
    {
        Time.timeScale = 1f;
    }
}

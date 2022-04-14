using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls when you go to next stage of game
/// </summary>
public class LevelController : MonoBehaviour
{
    public GameObject loseScreen;
    public bool gameOver = false;

    public void Start()
    {
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    public void LoseLevel()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0.3f;
        gameOver = true;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("Exit Function was Called!");
        Application.Quit();
    }
}

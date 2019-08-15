using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public GameObject playerPrefab;
    public GameObject lineRed;
    public GameObject lineOrange;
    public GameObject lineYellow;
    public GameObject lineGreen;
    public GameObject lineBlue;

    public Transform lineSpawnPos;

    private void Awake()
    {
        KeepGameManager();
    }

    public void RestartGame()
    {
        //destroy old game objects
        Destroy(GameObject.FindGameObjectWithTag("Ball"));
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

        //instantiate new game objects
        Instantiate(playerPrefab);
        Instantiate(lineRed);
        Instantiate(lineOrange);
        Instantiate(lineYellow);
        Instantiate(lineGreen);
        Instantiate(lineBlue);
    }

    private void KeepGameManager() //keeps the GameManager across scenes
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
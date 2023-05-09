using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Spawner spawner;
    public GameObject title;
    private Vector2 screenBounds;
    public GameObject playerPrefab;
    public GameObject player;
    private bool gameStarted = false;
    public GameObject splash;
    public GameObject scoreSystem;
    public Text scoreText;
    public int pointsWorth = 1;
    private int score;
    public Text gameOverText;
    public Text playAgainText;
    public GameObject bombObject;
    void Start()
    {
       spawner.active = false;
       title.SetActive(true); 
       scoreText.text = "Score: 0";
    }
    void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = playerPrefab;
        scoreText.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
        {
            if(Input.anyKeyDown)
            {
                ResetGame();
            }
        } else
        {
            if(!player)
            {
                print("pain");
                OnPlayerKilled();
            }
        }
        print(gameStarted);
        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");
        foreach(GameObject bombObject in nextBomb)
        {   if (!gameStarted)
            {
                Destroy(bombObject);
            } else if (bombObject.transform.position.y < (-screenBounds.y))
            {
                scoreSystem.GetComponent<Score>().AddScore(pointsWorth);
                Destroy(bombObject);
                scoreText.text = "Score: " + scoreSystem.GetComponent<Score>().score;
                print("score: " + scoreSystem.GetComponent<Score>().score);
            }
        }
    }
    void ResetGame()
    {
        spawner.active = true;
        title.SetActive(false);
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        gameStarted = true;
        splash.SetActive(false);
        scoreText.enabled = true;
        scoreSystem.GetComponent<Score>().score = 0;
        scoreText.text = "Score: 0";
        scoreSystem.GetComponent<Score>().Start();
    }
    void OnPlayerKilled()
    {
        spawner.active = false;
        gameStarted = false;
        splash.SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;

    readonly static float maxSpeed = 6;
    readonly static float maxDificulty = 5;

    public static float gSpeed = 3;
    public static float dificulty = 3;
    public static float interval = 0;
    public static float score = 0;
    public static bool paused = false;
    public static bool gameOver = false;
    public enum Event { RandomRocks, Octopus, GhostShip }


    public MoveDelete[] wonders;
    public MoveDelete[] rocks;
    public MoveDelete octopus;
    public MoveDelete ghostShip;
    public MoveDelete barrel;
    public TextMeshProUGUI scoreText;
    public GameObject pauseMenu;
    public GameObject gameOverImage;
    public GameObject continueMsg;

    private void Awake()
    {
        GM = this;
        gSpeed = 3;
        dificulty = 1;
        score = 0;
        paused = false;
        gameOver = false;
        interval = 2f;
        StartCoroutine(UpdatingScore());
        InitEvent(Event.RandomRocks);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public static void GameOver()
    {
        gameOver = true;
        GM.gameOverImage.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EscPressed()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    float response = 2f;

    private void Update()
    {
        if (interval < 0)
        {
            InitEvent((Event)Random.Range(0, 3));
            if (dificulty < maxDificulty) dificulty += .5f;
            else if (gSpeed < maxSpeed) gSpeed += .25f;
        }
        else
        {
            interval -= Time.deltaTime;
        }

        score += Time.deltaTime * Mathf.Pow(2, gSpeed) * Mathf.Pow(2, dificulty);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscPressed();
        }

        if (gameOver)
        {
            if (response <= 0)
            {
                if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                response -= Time.deltaTime;
                if (response <= 0) continueMsg.SetActive(true);
            }
        }
        

    }
    
    IEnumerator UpdatingScore()
    {
        while (true)
        {
            scoreText.text = ((int)score).ToString() + " km";
            SpawnWonder();
            yield return new WaitForSeconds(.5f);
            if (gameOver) break;
        }
    }

    int nextWonder = 0;

    void SpawnWonder()
    {
        if (score < nextWonder * 5714) return;
        Instantiate(wonders[nextWonder % 7], new Vector2(10, 2.25f), Quaternion.identity).SetSpeed(gSpeed);
        nextWonder++;
    }

    public static void InitEvent(Event type)
    {
        switch (type)
        {
            case Event.RandomRocks:
                RandomRocks();
                break;
            case Event.Octopus:
                Octopus();
                break;
            case Event.GhostShip:
                GhostShip();
                break;
        }
    }

    static void RandomRocks()
    {
        List<Vector2> positions = new List<Vector2>();
        int n = (int)(dificulty * 5);
        int b = Random.Range(1, n + 1);

        while (n > 0)
        {
            bool canSpawn = true;

            int x = Random.Range(10, 30);
            int y = Random.Range(-5, 2);

            for (int j = 0; j < positions.Count; j++)
            {
                if (positions[j].x == x && positions[j].y == y)
                {
                    canSpawn = false;
                }
            }

            if (canSpawn)
            {
                if (n != b)
                    Instantiate(GM.rocks[Random.Range(0, 2)], new Vector2(x, y), Quaternion.identity).SetSpeed(gSpeed);
                else
                    Instantiate(GM.barrel, new Vector2(x, y), Quaternion.identity).SetSpeed(gSpeed);
                n--;
                positions.Add(new Vector2(x, y));
            }
        }

        interval = 1 / gSpeed * 30;
    }

    static void Octopus()
    {

        int y = Random.Range(-4, 1);
        Instantiate(GM.octopus, new Vector2(10, y), Quaternion.identity).SetSpeed(gSpeed);
        interval = 1 / gSpeed * 20;
    }

    static void GhostShip()
    {
        List<Vector2> positions = new List<Vector2>();
        int n = (int)(dificulty);

        while (n > 0)
        {
            int x = (Random.Range(0, 2) * 2 - 1);
            int y = Random.Range(-4, 1);

            bool canSpawn = true;

            for (int j = 0; j < positions.Count; j++)
            {
                if (positions[j].x == x * 10 && positions[j].y == y)
                {
                    canSpawn = false;
                }
            }

            if (canSpawn)
            {
                Instantiate(GM.ghostShip, new Vector2(x * 10, y), Quaternion.identity).SetSpeed(x * gSpeed, x > 0);
                interval = 1 / gSpeed * 20;
                n--;
                positions.Add(new Vector2(x * 10, y));
            }
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spaceship : MonoBehaviour {
    //SPACESHIP Variabes
    public float speed;
    public float lives;
    public static int score;
    
    public bool gameOver;
    public Text scoreText;
    public Text livesText;
    public Text highScoreText;

    private int highScore;
    private float timer;
    private Rigidbody rb;

    //GUI Variables
    public int buttonWidth;
    public int buttonHeight;

    private int origin_x;
    private int origin_y;
//    private CursorLockMode currentMode;

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        lives = 3;
        score = 0;
        highScore = 0;
        highScore = PlayerPrefs.GetInt("High Score");
        SetScoreText();
        SetLivesText();
        StartCoroutine("RaiseScore");
        buttonWidth = 200;
        buttonHeight = 50;
        origin_x = Screen.width / 2 - buttonWidth / 2;
        origin_y = Screen.height / 2 - buttonHeight * 2;
    }

    void Update()
    {
        SetScoreText();
        if (score > 300 && score < 500)
        {
            enemy.spawnCount = 2;
            enemy.spawnRate = 0.7f;
            UFO.ufoSpeed = 2;
        }
        else if(score > 500 && score < 1000)
        {
            enemy.spawnRate = 0.4f;
            UFO.ufoSpeed = 4;
        }
        else if(score > 1000)
        {
            enemy.spawnRate = 0.3f;
            UFO.ufoSpeed = 6;
        }
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0f, moveHorizontal);
        rb.velocity = movement * speed;

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            lives--;
            SetLivesText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives < 1)
        {
            PlayerPrefs.SetInt("High Score", highScore);
            Time.timeScale = 0;
            gameOver = true;
        }
    }

    IEnumerator RaiseScore()
    {
        yield return new WaitForSeconds(1);
        score += 10;
        if(score > highScore)
        {
            highScore += 10;
        }
        StartCoroutine("RaiseScore");
    }

    void OnGUI()
    {
        if (gameOver)
        {
            if (GUI.Button(new Rect(origin_x, origin_y + 50, buttonWidth, buttonHeight), "Play Again?"))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight + 60, buttonWidth, buttonHeight), "Main Menu"))
            {
                SceneManager.LoadScene(0);
            }
            if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * 2 + 70, buttonWidth, buttonHeight), "Exit"))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
            }
        }
    }
}


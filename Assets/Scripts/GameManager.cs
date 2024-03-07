using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    private float startTime;

    private int score;
    public int Score => score;

    private void Awake() {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
            Pause();
        }
    }

    public void Play() {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        startTime = Time.time;
        
        Time.timeScale = 1f;
        player.enabled = true;

         Lobang[] lobangs = FindObjectsOfType<Lobang>();

        for (int i = 0; i < lobangs.Length; i++) {
            Destroy(lobangs[i].gameObject);
        }
    }

    public void GameOver() {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        float elapsedTime = Time.time - startTime;
        int scoreToAdd = Mathf.FloorToInt(elapsedTime * 5f);
        if (scoreToAdd > score) {
            score = scoreToAdd;
            scoreText.text = score.ToString();
        }
    }

     public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

}

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
    
    [SerializeField] private AudioSource carSound;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource crashSound;

    private float startTime;
    private float lastTimeCheck; // menyimpan cek waktu terakhir

    private int score;
    public int Score => score;

    public int scoreIncreaseRate = 20;

    // perlu variabel ini supaya tiap lubang punya kecepatan sama
    // jika tidak, lubang yang spawn duluan akan lebih cepat dari lubang lain
    private float lobangSpeed = 8.9f;  

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

    void Update()
    {
        IncreaseScore();

        // tambahkan kecepatan lobang dan jalan setiap 0.2 detik
        if (Time.time - lastTimeCheck > 0.2f)
        {
            lastTimeCheck = Time.time;

            Lobang[] lobangs = FindObjectsOfType<Lobang>();
            lobangSpeed += 0.1f;
            foreach (Lobang lobang in lobangs)
            {
                lobang.speed = lobangSpeed;
            }

            FindObjectOfType<Parallax>().animationSpeed += 0.07f / lobangSpeed;
        }
    }

    public void Play() {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        
        player.enabled = true;

        Time.timeScale = 1f;
        startTime = Time.time;
        lastTimeCheck = Time.time;

        Lobang[] lobangs = FindObjectsOfType<Lobang>();

        for (int i = 0; i < lobangs.Length; i++) {
            Destroy(lobangs[i].gameObject);
        }

        // play carSound and backgroundMusic
        carSound.Play();
        backgroundMusic.Play();

        // reset speed background dan lobang
        lobangSpeed = 8.9f; 
        FindObjectOfType<Parallax>().animationSpeed = 0.5f;
    }

    public void GameOver() {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        // play car crash sound effect here and stop others
        crashSound.Play();
        carSound.Stop();
        backgroundMusic.Stop();

        Pause();
    }

    public void IncreaseScore()
    {
        float timeSinceStart = (Time.time - startTime) * scoreIncreaseRate;
        score = (int) timeSinceStart;
        // Debug.Log("deltaTime = " + Time.deltaTime);

        scoreText.text = score.ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

}

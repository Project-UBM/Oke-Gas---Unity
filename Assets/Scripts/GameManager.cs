using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    [SerializeField] private Player player;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;

    private int score;
    public int Score => score;

    public void Play() {
        // score = 0;
        // scoreText.text = score.ToString();

        // playButton.SetActive(false);
        // gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Lobang[] lobang = FindObjectsOfType<Lobang>();

        for (int i = 0; i < lobang.Length; i++) {
            Destroy(lobang[i].gameObject);
        }
    }

    public void GameOver() {
        // playButton.SetActive(true);
        // gameOver.SetActive(true);
        Debug.Log("Game Over Trigered");
        Pause();
    }

    public void Pause() {
        Debug.Log("Game Over Trigered");

        Time.timeScale = 0f;
        player.enabled = false;
    }
}

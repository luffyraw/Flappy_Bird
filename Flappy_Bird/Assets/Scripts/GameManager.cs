using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    private int score;

    public GameObject flappybird;
    public GameObject nextOption;
    public GameObject backOption;
    public GameObject _getready;
    
   

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fly, deal, scor;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
      
    }
    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOver.SetActive(false);
        _getready.SetActive(false);
        
        nextOption.SetActive(false);
        backOption.SetActive(false);
        flappybird.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for(int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }
    public void GameOver()
    {
        
        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
        audioSource.PlayOneShot(deal);
    }
    public void Pause()
    {
        
        Time.timeScale = 0f;
        player.enabled = false;
    }
    
    public void IncreaseScore()
    {
        audioSource.PlayOneShot(scor);
        score++;
        scoreText.text = score.ToString();
       
    }
}

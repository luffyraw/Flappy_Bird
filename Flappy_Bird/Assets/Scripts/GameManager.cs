using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject restart;
    private int score;
    public GameObject Score;
    public GameObject flappybird;
    public GameObject nextOption;
    public GameObject backOption;
    public GameObject _readyImg;
    public GameObject _getImg;



    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip deal, scor;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
      
    }
    public void Play()
    {
        Score.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        restart.SetActive(false);

        _readyImg.SetActive(false);
        _getImg.SetActive(false);
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
        restart.SetActive(true);
        //Pause();2
        if(Player.instance.countAudioDie == 1)
        {
            audioSource.PlayOneShot(deal);
        }
    }
    public void ReStart()
    {
        SceneManager.LoadScene(0);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image livesImageDisplay;

    public GameObject titleScreen;

    public Text scoreText;
    public int score = 0;

    public Text levelText;
    public int level = 1;

    public int startingPitch = 1;
    private AudioSource _audioSource;

    private int counter = 0;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
       _audioSource = GetComponent<AudioSource>();

        //Initialize the pitch
        _audioSource.pitch = startingPitch;
    }


public void UpdateLives(int currentLives)
    {
        Debug.Log("Lives Remaining:" + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        counter++;
        scoreText.text = "Score: " + score;

    }

    public void UpdateLevel()
    {
        if (counter >= 10)
        {
            counter = 0;
            level++;
            levelText.text = "Level:" + level;
            _audioSource.pitch += 0.05f;
        }
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score:" + score;
        levelText.text = "Level:" + 1;
    }
}

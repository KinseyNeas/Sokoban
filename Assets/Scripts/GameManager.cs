using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Buttons
    public Button startButton, nextButton, prevButton;
    public List<Button> restartButtons;
    public List<Button> mainMenuButtons;
    // Panels
    public GameObject PausePanel;
    public GameObject LevelCompletePanel;
    public GameObject EndGamePanel;
    // Level Info
    public int levelNumber;
    private bool levelComplete = false;
    private int score;
    public TextMeshProUGUI levelText;
    private List<Vector3> StartingPositions = new List<Vector3>();
    // Level Characters
    public GameObject Player;
    public List<GameObject> Boxes;

    private void Start()
    {
        // Adding listeners to buttons
        if(startButton != null) startButton.onClick.AddListener(() => StartGame());
        if(nextButton != null) nextButton.onClick.AddListener(() => NextLevel());
        if(prevButton != null) prevButton.onClick.AddListener(() => PrevLevel());
        if(restartButtons != null) {
            foreach(Button button in restartButtons) button.onClick.AddListener(() => RestartLevel());
        }
        if(mainMenuButtons != null) {
            foreach(Button button in mainMenuButtons) button.onClick.AddListener(() => MainMenu());
        }

        if(levelText != null) levelText.text = "Level " + levelNumber;
        if(Player != null && Boxes != null) CatalogStartingPositions();
    }
    private void Update(){
        if(score != 0 && score == Boxes.Count) {
            levelComplete = true;
            Debug.Log("You did it!");
        }
        if(levelComplete && LevelCompletePanel != null){
            if(levelNumber == 3){
                EndGamePanel.SetActive(true);
            } else {
                LevelCompletePanel.SetActive(true);
                levelComplete = false;
            } 
        }
    }
    private void StartGame(){
        SceneManager.LoadScene("Level1");
    }
    private void NextLevel(){
        if(levelNumber < 3){
            SceneManager.LoadScene("Level" + (levelNumber + 1));
        }
    }
    private void PrevLevel(){
        if(levelNumber > 1){
            SceneManager.LoadScene("Level" + (levelNumber - 1));
        }
    }
    private void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    private void RestartLevel(){
        Player.transform.position = StartingPositions[0];
        Player.transform.up = Vector3.zero;
        Player.GetComponent<PlayerBehavior>().targetPosition.position = StartingPositions[0];
        int boxNum = 1;
        foreach(GameObject box in Boxes){
            box.gameObject.transform.position = StartingPositions[boxNum];
            ++boxNum;
        }
        if(PausePanel != null && PausePanel.activeSelf) PausePanel.SetActive(false);
        if(LevelCompletePanel != null && LevelCompletePanel.activeSelf) {
            levelComplete = false;
            score = 0;
            LevelCompletePanel.SetActive(false);
        }
    }
    public void AddToScore(){
        score+=1;
    }
    public void SubFromScore(){
        score-=1;
    }
    private void CatalogStartingPositions(){
        StartingPositions.Add(Player.transform.position);
        foreach(GameObject box in Boxes){
            StartingPositions.Add(box.transform.position);
        }
    }
}

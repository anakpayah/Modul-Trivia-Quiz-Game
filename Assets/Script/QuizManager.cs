using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private List<QuizDataSO> quizData;
    [SerializeField] private float timeLimit = 60f;


    private List<Question> questList;
    private Question selectedQuest;
    private int scoreCount = 0;
    private float currentTime;
    private int lifePoint = 3;

    
    private GameStatus gameStatus = GameStatus.Next;
    public GameStatus @GameStatus { get { return gameStatus; } }
    public void StartGame(int index)
    {
        scoreCount = 0;
        currentTime = timeLimit;
        lifePoint = 3;

        questList = quizData[index].questions;
        RandQuestion();

        gameStatus = GameStatus.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus == GameStatus.Playing) {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    void SetTimer(float value) {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "Time:" + time.ToString("mm':'ss");

        if (currentTime <= 0) {
            gameStatus = GameStatus.Next;
            quizUI.GameOverPanel.SetActive(true);
        }
    }

    void RandQuestion() {
        int val = UnityEngine.Random.Range(0, questList.Count);
        selectedQuest = questList[val];

        quizUI.SetQuestion(selectedQuest);

        questList.RemoveAt(val);
    }

    public bool Answer(string answer) {
        bool corrected = false;

        if (answer == selectedQuest.correctAns) {
            //Yes
            corrected = true;
            scoreCount += 100;

            quizUI.ScoreText.text = "Score: " + scoreCount;
        }
        else {
            //No
            lifePoint--;
            quizUI.ReduceLife(lifePoint);
            if (lifePoint <= 0) {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
            }
        }

        if (gameStatus == GameStatus.Playing) {
            if (questList.Count > 0) {
                Invoke("RandQuestion",1f);
            }
            else {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
            }
        }

        return corrected;
    }
}

[System.Serializable]
public class Question {
    public string questionInfo;
    public List<string> options;
    public string correctAns;
    public QuestionType QType;
    public Sprite questionImg;
    public AudioClip questionAud; 
}

[System.Serializable]
public enum QuestionType {
    TEXT,
    IMAGE,
    AUDIO
}

[System.Serializable]
public enum GameStatus {
    Next,
    Playing
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private List<Question> questList;
    private Question selectedQuest;
    // Start is called before the first frame update
    void Start()
    {
        RandQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandQuestion() {
        int val = Random.Range(0, questList.Count);
        selectedQuest = questList[val];

        quizUI.SetQuestion(selectedQuest);
    }
    public bool Answer(string answer) {
        bool corrected = false;

        if (answer == selectedQuest.correctAns) {
            //Yes
            corrected = true;
        }
        else {
            //No
        }
        Invoke("RandQuestion",1f);

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
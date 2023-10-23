using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager QM;
    [SerializeField] private TMP_Text questionText, scoreText, timerText;
    [SerializeField] private List<Image> lifeList;
    [SerializeField] private GameObject mainMenuPanel, gamePlayPanel, gameOverPanel;
    [SerializeField] private Image questionImage;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private List<Button> options, uiButtons;
    [SerializeField] private Color correctColor, wrongColor, normalColor;

    private float audioLength;
    private Question question;
    private bool answered;
    
    public TMP_Text ScoreText { get {return scoreText; } }
    public TMP_Text TimerText { get {return timerText; } }
    public GameObject GameOverPanel {get { return gameOverPanel; } }

    // Start is called before the first frame update
    private void Awake() {
        for (int i = 0 ; i < options.Count ; i++) {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
        for (int i = 0 ; i < uiButtons.Count ; i++) {
            Button localBtn = uiButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuestion(Question question) {
        this.question = question;

        switch (question.QType) {
            case QuestionType.TEXT:
                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                ImageHolder();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.questionImg;
                break;
            case QuestionType.AUDIO:
                ImageHolder();
                questionAudio.transform.gameObject.SetActive(true);
                audioLength = question.questionAud.length;
                StartCoroutine(PlayAudio());
                break;    
        }

        questionText.text = question.questionInfo;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0 ; i < options.Count ; i++) {
            options[i].GetComponentInChildren<TMP_Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalColor;
        }

        answered = false;

    }

    IEnumerator PlayAudio() {
        if(question.QType == QuestionType.AUDIO) {
            questionAudio.PlayOneShot(question.questionAud);

            yield return new  WaitForSeconds(audioLength + 0.5f);

            StartCoroutine(PlayAudio());
        }
        else {
            StopCoroutine(PlayAudio());
            yield return null ;
        }
    }

    void ImageHolder () {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
    }

    void OnClick(Button btn){
        if (QM.GameStatus == GameStatus.Playing) {
            if (!answered) {
                answered = true;
                bool val = QM.Answer(btn.name);

                if (val) {
                    btn.image.color = correctColor;
                }
                else {
                    btn.image.color = wrongColor;
                }
            }
        }

        switch (btn.name) {
            case "Biologi":
                QM.StartGame(0);
                mainMenuPanel.SetActive(false);
                gamePlayPanel.SetActive(true);
                break;
            case "Fisika":
                QM.StartGame(1);
                mainMenuPanel.SetActive(false);
                gamePlayPanel.SetActive(true);
                break;
            case "Kimia":
                QM.StartGame(2);
                mainMenuPanel.SetActive(false);
                gamePlayPanel.SetActive(true);
                break;
        }
    }

    public void RetryButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReduceLife(int index) {
        lifeList[index].color = wrongColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvoScript : MonoBehaviour
{
    [Header("Questions")]
    [TextArea(2, 10)]
    public List<string> questions;
    [Header("Left Ans")]
    public List<Answer> leftAnswers;
    [Header("Mid Ans")]
    public List<Answer> midAnswers;
    [Header("Right Ans")]
    public List<Answer> rightAnswers;

    public Text question;
    public Text left;
    public Text mid;
    public Text right;

    public Color black;
    public Color gray;

    GameObject managerObject;
    GameManager managerScript;

    public int questionPicker;
    public int maxQuestions;

    int highlight;

    bool startofgame;

    public AudioClip clickClip;
    public AudioSource clickSource;

    // Start is called before the first frame update
    void Start()
    {
        managerObject = GameObject.Find("GameManager");
        managerScript = managerObject.GetComponent<GameManager>();
        maxQuestions = questions.Count;

        highlight = 1;

        startofgame = true;
        NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        switch (highlight)
        {
            case 1:
                left.color = black;
                mid.color = gray;
                right.color = gray;

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    highlight = 3;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    highlight = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    NextQuestion(FindAnswer(questionPicker));
                }
                break;
            case 2:
                left.color = gray;
                mid.color = black;
                right.color = gray;

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    highlight = 1;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    highlight = 3;
                }
                else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    NextQuestion(FindAnswer(questionPicker));
                }
                break;
            case 3:
                left.color = gray;
                mid.color = gray;
                right.color = black;

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    highlight = 2;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    highlight = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    NextQuestion(FindAnswer(questionPicker));
                }
                break;
        }
    }

    public void NextQuestion(Answer answer = null)
    {
        if (startofgame)
        {
            questionPicker = Random.Range(0, (questions.Count));
            DisplayNewQuestion(questionPicker);
            startofgame = false;
        }
        else if (!startofgame && questions.Count > 1)
        {
            clickSource.PlayOneShot(clickClip);
            managerScript.AddPoints(answer);
            DeletePreviousQuestion(questionPicker);
            questionPicker = Random.Range(0, (questions.Count));
            DisplayNewQuestion(questionPicker);
        }
        else
        {
            clickSource.PlayOneShot(clickClip);
            managerScript.AddPoints(answer);
            managerScript.EndQuiz();
        }
    }

    void DeletePreviousQuestion(int qNum)
    {
        questions.Remove(questions[qNum]);
        leftAnswers.Remove(leftAnswers[qNum]);
        rightAnswers.Remove(rightAnswers[qNum]);
        midAnswers.Remove(midAnswers[qNum]);
        Debug.Log("kill question");
        
    }

    void DisplayNewQuestion(int qNum)
    {
        question.text = questions[qNum];
        left.text = leftAnswers[qNum].ans;
        mid.text = midAnswers[qNum].ans;
        right.text = rightAnswers[qNum].ans;
    }

    Answer FindAnswer(int qNum)
    {
        Answer answer;
        if(highlight == 1)
        {
            answer = leftAnswers[qNum];
        }
        else if (highlight == 2)
        {
            answer = midAnswers[qNum];
        }
        else
        {
            answer = rightAnswers[qNum];
        }

        return answer;
    }
}

[System.Serializable]
public class Answer
{
    public string ans;
    public enum element {water, wind, fire, light, twilight, none};
    public element primaryElement;
    public element secondaryElement;
    public int primaryAmount;
    public int secondaryAmount;

    public Answer()
    {
        primaryAmount = 2;
        secondaryAmount = 1;
    }
}
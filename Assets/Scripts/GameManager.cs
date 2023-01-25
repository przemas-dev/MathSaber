using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<XmlNodeList> questionsList;
    public List<Question> questions;
    public Queue<Question> Questions = new();
    public QuestionManager QuestionManager;
    public Question ActiveQuestion;
    public TextMeshPro QuestionLabel;
    public TextMeshPro PointsLabel;
    public bool delayBetweenStages = false;
    public bool endOfGame = false;
    public float DistanceBetweenQuestioin = 10f;
    public float Velocity = 2f;
    public float TimeBetweenQuestion => DistanceBetweenQuestioin / Velocity;
    public float TimeBeforeStage = 10.0f;
    public int currentStage;
    public int absoluteNumberOfStages = 3;
    public int startingNumberOfStage = 3;
    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            PointsLabel.text = $"Points: {_points}";
        }
    }

    private int _points;
    private float _delayTime = 0;
    private float _timeSinceLastQuestion = 0;

    void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        endOfGame = false;
        currentStage = startingNumberOfStage;
        questions = Stage(currentStage);
        delayBetweenStages = true;
        Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EndOfStage();

        CountingTime();
        

        if (_timeSinceLastQuestion >= TimeBetweenQuestion && currentStage <= absoluteNumberOfStages)
        {
            Questions.Enqueue(QuestionManager.SpawnNewQuestion(Velocity, questions, currentStage));
            _timeSinceLastQuestion = 0f;
        }

        if(_delayTime >= TimeBeforeStage && currentStage <= absoluteNumberOfStages)
        {
            ActiveQuestion = QuestionManager.SpawnNewQuestion(Velocity, questions, currentStage);
            QuestionLabel.text = ActiveQuestion.QuestionText;
            _delayTime = 0;
            delayBetweenStages = false;
        }

    }

    private void IncreaseSpeed(float value = 0.5f)
    {
        Velocity += value;
        foreach (var answerCube in Questions.SelectMany(question => question.AnswerCubes))
        {
            answerCube.Velocity = Velocity;
        }
    }

    public void AnswerActiveQuestion(bool isCorrect)
    {
        Points += isCorrect ? 1 : 0;
        //Debug.Log(isCorrect ? "Correct Answer!" : "Incorrect Answer!");
        if (isCorrect)
        {
            IncreaseSpeed();
        }
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        ActiveQuestion = Questions.Dequeue();
        QuestionLabel.text = ActiveQuestion.QuestionText;
    }

    public void ShowStageNumber()
    {
        if(endOfGame)
        {
            QuestionLabel.text = $"Thanks for playing!";
            SceneManager.LoadScene("MainMenu");
        }
        else
            QuestionLabel.text = $"Stage {currentStage}";
    }

    public void CountingTime()
    {
        if (delayBetweenStages)
        {
            _delayTime += Time.deltaTime;

            if (_delayTime >= Velocity)
                ShowStageNumber();
        }
        else
        {
            _timeSinceLastQuestion += Time.deltaTime;
        }
    }

    private void EndOfStage()
    {
        if (questions.Count <= 0 && currentStage <= absoluteNumberOfStages) 
        {
            currentStage++;
            questions = Stage(currentStage);
            delayBetweenStages = true;
        }

        if (currentStage > absoluteNumberOfStages)
        {
            endOfGame = true;
            delayBetweenStages = true;
        }

    }


    public List<Question> Stage(int currentStage)
    {
        string xmlString = File.ReadAllText($"Assets/Resources/XML/stage{currentStage}.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(Stage));
        List<Question> questions;

        using (StringReader reader = new StringReader(xmlString))
        {
            Stage stage = (Stage)serializer.Deserialize(reader);

            questions = stage.Questions;
        }
        return questions;
    }

}

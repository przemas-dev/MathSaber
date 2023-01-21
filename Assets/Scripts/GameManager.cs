using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<XmlNodeList> questionsList;
    public QuestionManager QuestionManager;
    public Question ActiveQuestion;
    public float DistanceBetweenQuestioin = 10f;
    public float Velocity = 2f;
    public float TimeBetweenQuestion => DistanceBetweenQuestioin / Velocity;
    public Queue<Question> Questions = new();
    List<Question> questions;
    public TextMeshPro QuestionLabel;
    public TextMeshPro PointsLabel;
    public bool delayBetweenStages = false;
    private int _points;
    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            PointsLabel.text = $"Points: {_points}";
        }
    }

    public int currentStage;



    private float _timeSinceLastQuestion = 0;
    void Start()
    {
        currentStage = 2;
        questions = Stage(currentStage);
        StartNewGame();
    }

    private void StartNewGame()
    {
        ActiveQuestion = QuestionManager.SpawnNewQuestion(Velocity, questions, currentStage);
        QuestionLabel.text = ActiveQuestion.QuestionText;
        Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (questions.Count <= 0)
        {
            currentStage++;
            questions = Stage(currentStage);
        }

        _timeSinceLastQuestion += Time.deltaTime;
        if (_timeSinceLastQuestion >= TimeBetweenQuestion)
        {
            Questions.Enqueue(QuestionManager.SpawnNewQuestion(Velocity, questions, currentStage));
            _timeSinceLastQuestion = 0f;
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

    private void ShowNextQuestion()
    {
        ActiveQuestion = Questions.Dequeue();
        QuestionLabel.text = ActiveQuestion.QuestionText;
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

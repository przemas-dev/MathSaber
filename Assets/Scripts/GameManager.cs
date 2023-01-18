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
    List<QuestionsXML> questionsXML;
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

    private int currentStage;



    private float _timeSinceLastQuestion = 0;
    void Start()
    {
        currentStage = 1;
        questionsXML = Stage(currentStage);
        StartNewGame();
    }

    private void StartNewGame()
    {
        ActiveQuestion = QuestionManager.SpawnNewQuestion(Velocity, questionsXML, currentStage);
        QuestionLabel.text = ActiveQuestion.QuestionText;
        Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (questionsXML.Count <= 0)
        {
            currentStage++;
            questionsXML = Stage(currentStage);
        }

        _timeSinceLastQuestion += Time.deltaTime;
        if (_timeSinceLastQuestion >= TimeBetweenQuestion)
        {
            Questions.Enqueue(QuestionManager.SpawnNewQuestion(Velocity, questionsXML, currentStage));
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

    public List<QuestionsXML> Stage(int currentStage)
    {
        string xmlString = File.ReadAllText($"Assets/Resources/XML/stage{currentStage}.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(Quiz));
        List<QuestionsXML> questions;

        using (StringReader reader = new StringReader(xmlString))
        {
            Quiz quiz = (Quiz)serializer.Deserialize(reader);

            questions = quiz.questionsXML;
        }
        return questions;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestionManager QuestionManager;
    public Question ActiveQuestion;
    public float DistanceBetweenQuestioin = 5f;
    public float Velocity = 2f;
    public float TimeBetweenQuestion => DistanceBetweenQuestioin / Velocity;
    public Queue<Question> Questions = new();
    public TextMeshPro QuestionLabel;
    public TextMeshPro PointsLabel;
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

    
    private float _timeSinceLastQuestion = 0;
    void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        ActiveQuestion = QuestionManager.SpawnNewQuestion(Velocity);
        QuestionLabel.text = ActiveQuestion.QuestionText;
        Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastQuestion += Time.deltaTime;
        if (_timeSinceLastQuestion >= TimeBetweenQuestion)
        {
            Questions.Enqueue(QuestionManager.SpawnNewQuestion(Velocity));
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
        Debug.Log(isCorrect ? "Correct Answer!" : "Incorrect Answer!");
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

}

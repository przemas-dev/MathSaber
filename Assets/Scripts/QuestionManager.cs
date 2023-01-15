using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public Transform[] SpawnPoints = new Transform[3];
    public GameObject AnswerCubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Question GetNewQuestion()
    {
        //TODO: real generating questions
        return new Question()
        {
            QuestionText = "Ile to 2 x 2?",
            AnswerTexts = new[] { "2", "4", "6" },
            CorrectAnswer = 1
        };
    }
    
    
    public Question SpawnNewQuestion(float velocity)
    {
        var question = GetNewQuestion();
        
        for (var i = 0; i < SpawnPoints.Length; i++)
        {
            var spawnPoint = SpawnPoints[i];
            var go = Instantiate(AnswerCubePrefab, spawnPoint.position, Quaternion.identity);
            var answerCube = go.GetComponent<AnswerCube>();
            answerCube.Velocity = velocity;
            answerCube.AnswerText = question.AnswerTexts[i];
            question.AnswerCubes[i] = answerCube;
        }

        question.AnswerCubes[question.CorrectAnswer].IsCorrect = true;
        return question;
    }
}

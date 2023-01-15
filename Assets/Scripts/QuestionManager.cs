using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    private Question GetNewQuestion(List<QuestionsXML> QuestionsList)
    {
        int randQuestion = Random.Range(0, QuestionsList.Count);

        Question question = new Question()
        {
            QuestionText = QuestionsList[randQuestion].question,
            AnswerTexts = new[]
            {
                QuestionsList[randQuestion].ans1,
                QuestionsList[randQuestion].ans2,
                QuestionsList[randQuestion].ans3
            },
            CorrectAnswer = QuestionsList[randQuestion].correct
        };

        QuestionsList.RemoveAt(randQuestion);

        return question;
    }
    
    
    public Question SpawnNewQuestion(float velocity, List<QuestionsXML> QuestionsList)
    {
        var question = GetNewQuestion(QuestionsList);
        
        for (var i = 0; i < SpawnPoints.Length; i++)
        {
            var spawnPoint = SpawnPoints[i];
            var go = Instantiate(AnswerCubePrefab, spawnPoint.position, Quaternion.identity);
            var answerCube = go.GetComponent<AnswerCube>();
            answerCube.Velocity = velocity;
            answerCube.AnswerText = question.AnswerTexts[i];
            question.AnswerCubes[i] = answerCube;
        }

        question.AnswerCubes[question.CorrectAnswer-1].IsCorrect = true;
        return question;
    }
}

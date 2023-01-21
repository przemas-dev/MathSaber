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
    public int stage;
    public int randQuestion = 0;
    public int counterFirstStage = 0;
    public int firstStageNumberOfQuestions = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Question GetNewQuestion(List<Question> QuestionsList)
    {
        if (stage != 1)
        {
            randQuestion = Random.Range(0, QuestionsList.Count);
        }
        else if (stage == 1 && counterFirstStage == 0) 
        {
            randQuestion = Random.Range(0, firstStageNumberOfQuestions);
            counterFirstStage++;
        }
        else if(stage == 1 && counterFirstStage < 3)
        {
            randQuestion += firstStageNumberOfQuestions;
            counterFirstStage++;
        }

        var question = QuestionsList[randQuestion];

        if (stage != 1) 
        {
            QuestionsList.RemoveAt(randQuestion);
        }
        else if (counterFirstStage > 2)
        {
            for (int i = randQuestion; i >= 0 ; i-= firstStageNumberOfQuestions) 
            {
                QuestionsList.RemoveAt(i);
            }
            firstStageNumberOfQuestions--;
            counterFirstStage = 0;
        }

        return question;
    }
    
    
    public Question SpawnNewQuestion(float velocity, List<Question> QuestionsList, int currentStage)
    {
        stage = currentStage; 
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

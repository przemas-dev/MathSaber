using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestionManager QuestionManager;
    [HideInInspector]
    public int Points = 0;
    public Question ActiveQuestion;
    public float Velocity = 0.1f;
    public Transform[] SpawnPoints = new Transform[3];
    public GameObject AnswerCubePrefab;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveQuestion == null)
        {
            ActiveQuestion = QuestionManager.GetQuestion();
            for (var i = 0; i < SpawnPoints.Length; i++)
            {
                var spawnPoint = SpawnPoints[i];
                var go = Instantiate(AnswerCubePrefab, spawnPoint.position, Quaternion.identity);
                var answerCube = go.GetComponent<AnswerCube>();
                answerCube.Velocity = Velocity;
                answerCube.AnswerText = ActiveQuestion.AnswerTexts[i];
            }
        }
    }
}

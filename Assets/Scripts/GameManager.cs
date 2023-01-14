using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
            ActiveQuestion = new Question();
            foreach (var spawnPoint in SpawnPoints)
            {
                var go = Instantiate(AnswerCubePrefab, spawnPoint.position, Quaternion.identity);
                go.GetComponent<AnswerCube>().Velocity = Velocity;
            }
        }
    }
}

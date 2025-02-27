using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AnswerCube"))
        {
            var answerCube = other.GetComponent<AnswerCube>();
            Destroy(other.gameObject);
            GameManager.AnswerActiveQuestion(answerCube.IsCorrect);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Question GetQuestion()
    {
        return new Question()
        {
            QuestionText = "Ile to 2 x 2?",
            AnswerTexts = new[] { "2", "4", "6" },
            CorrectAnswer = 2
        };
    }
}

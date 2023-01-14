using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Points = 0;
    public Question ActiveQuestion;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveQuestion == null)
        {
            ActiveQuestion = new Question();
        }
    }
}

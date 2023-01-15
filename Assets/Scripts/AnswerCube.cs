using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerCube : MonoBehaviour
{
    // Start is called before the first frame update


    public float Velocity;
    public TextMeshPro AnswerTextMesh;
    public bool IsCorrect;
    public string AnswerText
    {
        set => AnswerTextMesh.text = value;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -Velocity * Time.deltaTime);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager GameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variable of class
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown(speed);
    }

    public void MoveDown(float speed)
    {
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);
    }
}

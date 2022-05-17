using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float horizontalInput;

    private float xRange = 7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xRange, xRange), transform.position.y, transform.position.z);
    }
}

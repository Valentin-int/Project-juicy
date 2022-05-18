using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variable of class
    public float speed = 10;
    public float horizontalInput;
    public float life = 3;

    public EnemyController enemyController;
    public EnemyController enemyController1;
    public EnemyController enemyController2;

    private float xRange = 7;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GameObject.Find("Enemy").GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xRange, xRange), transform.position.y, transform.position.z);

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello");
        if (other.gameObject.CompareTag("Enemy"))
        {
            life -= enemyController.damage;
        }
        if (other.gameObject.CompareTag("Enemy 1"))
        {
            life -= enemyController1.damage;
        }
        if (other.gameObject.CompareTag("Enemy 2"))
        {
            life -= enemyController2.damage;
        }
    }
}

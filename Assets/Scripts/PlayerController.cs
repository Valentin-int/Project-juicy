using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variable of class
    public float speed = 10;
    public float horizontalInput;
    public float life = 3;
    public float backAfterCollision;
    public bool gameOver;

    public EnemyController enemyController;
    public EnemyController enemyController1;
    public EnemyController enemyController2;

    private float xRange = 7;
    private bool touchable = true;
    private int outOfBound;

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
        OutOfBound();
        Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && touchable == true)
        {
            life -= enemyController.damage;
            EnemiesAttack();
        }
        if (other.gameObject.CompareTag("Enemy 1") && touchable == true)
        {
            life -= enemyController1.damage;
            EnemiesAttack();
        }
        if (other.gameObject.CompareTag("Enemy 2") && touchable == true)
        {
            life -= enemyController2.damage;
            EnemiesAttack();
        }
    }

    IEnumerator InvincibilityAfterCollision()
    {
        yield return new WaitForSeconds(1);
        touchable = true;
    }

    void EnemiesAttack()
    {
        transform.Translate(Vector3.back * Time.deltaTime * backAfterCollision);
        touchable = false;
        outOfBound++;
        StartCoroutine(InvincibilityAfterCollision());
    }

    void Death()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
            gameOver = true;
        }
    }

    void OutOfBound()
    {
        if(outOfBound >= 3)
        {
            Destroy(gameObject);
            gameOver = true;
        }
    }
}

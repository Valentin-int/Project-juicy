using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variable of class
    public float speed = 10;
    public float horizontalInput;
    public float backAfterCollision;
    public bool gameOver;

    public GameManager gameManager;

    private float xRange = 7;
    private bool touchable = true;
    public int outOfBound;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && touchable == true)
        {
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

    void OutOfBound()
    {
        if(outOfBound >= 3)
        {
            gameManager.player.gameObject.SetActive(false);
            outOfBound = 0;
            touchable = true;
            gameOver = true;
            gameManager.GameOver();
        }
    }
}

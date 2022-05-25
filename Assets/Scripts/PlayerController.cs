using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Variable of class
    public float speed = 10;
    public float jumpForce;
    public float horizontalInput;
    public float backAfterCollision;
    public bool gameOver;

    public GameManager gameManager;

    private Rigidbody playerRb;
    private SphereCollider playerCollider;

    private Vector3 scaleMin = new Vector3(0.5f, 1, 1);
    private Vector3 scaleMax = new Vector3(1.5f, 1.5f, 1.5f);
    private float colliderScaleMin = 0.25f;
    private float colliderScaleMax = 0.5f;
    private float xRange = 7;
    public bool touchable = true;
    public bool isOnGround = true;
    public int outOfBound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        SlimDown();
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

        if (other.gameObject.CompareTag("BonusZone"))
        {
            BonusScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
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

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    void SlimDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !gameOver)
        {
            transform.localScale = scaleMin;
            playerCollider.radius = colliderScaleMin;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && !gameOver)
        {
            transform.localScale = scaleMax;
            playerCollider.radius = colliderScaleMax;
        }
    }

    void BonusScore()
    {
        gameManager.score += gameManager.moreScore;
    }
}

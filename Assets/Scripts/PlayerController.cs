using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7.5f;
    public float coolDownTime = 5;

    CharacterController playerOne;
    CharacterController playerTwo;

    Vector2 rotationOne = Vector2.zero;
    Vector2 rotationTwo = Vector2.zero;

    // Start is called before the first frame update
    [HideInInspector]
    public bool canMove = true;
    private bool isMoving;

    Queue<string> powerUps;

    private int health = 100;
    private float coolDown = 0;


    void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("Rat").GetComponent<CharacterController>();
        playerTwo = GameObject.FindGameObjectWithTag("Scientist").GetComponent<CharacterController>();

        rotationOne.y = transform.eulerAngles.y;
        rotationTwo.y = transform.eulerAngles.y;

        powerUps = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(playerOne, "Player 1");
        MovePlayer(playerTwo, "Player 2");

        if (Input.GetButton("Jump"))
        {
            UsePowerUp();
        }
    }
    void MovePlayer(CharacterController c, string player)
    {
        float curSpeedX = canMove ? speed * Input.GetAxis(player + " Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis(player + " Horizontal") : 0;

        Vector3 moveDirection = (Vector3.forward * curSpeedX) + (Vector3.right * curSpeedY);

        c.Move(moveDirection * Time.deltaTime);
    }

/*    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Rat"))
        {
            if (other.CompareTag("RatObstacle"))
            {
                TakeDamage(10);
                Destroy(other.gameObject);
            }
            else powerUps.Enqueue(other.name);
        }

        if (gameObject.CompareTag("Scientist")) {
            if (other.CompareTag("ScientistObstacle")) TakeDamage(10);
            else powerUps.Enqueue(other.name);
        }
    }*/

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }

    private void UsePowerUp()
    {
        if (powerUps.Count > 0 && coolDown < Time.time)
        {
            coolDown = Time.time + coolDownTime;
            powerUps.Dequeue();
            Debug.Log("Power UP used");
        }
    }
}

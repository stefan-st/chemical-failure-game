using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7.5f;
    public float rotationSpeed = 2.0f;
    public float coolDownTime = 5;

    CharacterController playerOne;
    CharacterController playerTwo;

    Vector2 rotationOne = Vector2.zero;
    Vector2 rotationTwo = Vector2.zero;

    // Start is called before the first frame update
    [HideInInspector]
    public bool canMove = true;
    private bool isMoving;


    private int health = 100;
    private float coolDown = 0;


    void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("Rat").GetComponent<CharacterController>();
        playerTwo = GameObject.FindGameObjectWithTag("Scientist").GetComponent<CharacterController>();

        rotationOne.y = transform.eulerAngles.y;
        rotationTwo.y = transform.eulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(playerOne, "Player 1");
        MovePlayer(playerTwo, "Player 2");
    }
    void MovePlayer(CharacterController c, string player)
    {
        float curSpeedX = canMove ? speed * Input.GetAxis(player + " Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis(player + " Horizontal") : 0;

        Vector3 moveDirection = (Vector3.forward * curSpeedX) + (Vector3.right * curSpeedY);

        if (player == "Player 2")
        {
            // c.transform.Rotate((Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), (Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), 0, Space.World);
            rotationTwo.y += Input.GetAxis("Mouse X") * 2.0f;
            c.transform.eulerAngles = new Vector2(0, rotationTwo.y);
        }

        c.Move(moveDirection * Time.deltaTime);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}

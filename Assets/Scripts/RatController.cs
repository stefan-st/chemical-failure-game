using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    public float speed = 7.5f;

    CharacterController playerOne;
    CharacterController playerTwo;

    Vector3 moveDirectionOne = Vector3.zero;
    Vector2 rotationOne = Vector2.zero;
    Vector3 moveDirectionTwo = Vector3.zero;
    Vector2 rotationTwo = Vector2.zero;

    // Start is called before the first frame update
    [HideInInspector]
    public bool canMove = true;
    private bool isMoving;

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
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? speed * Input.GetAxis(player + " Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis(player + " Horizontal") : 0;

        if (curSpeedX != 0 || curSpeedY != 0)
        {
            isMoving = true;
        }

        Vector3 moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        c.Move(moveDirection * Time.deltaTime);
    }
}

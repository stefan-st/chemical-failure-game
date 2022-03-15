using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    public float coolDownTime = 3;
    public GameObject hamsterBall;
    // Start is called before the first frame update

    Queue<string> powerUps;

    private int health = 100;
    private float coolDown = 0;
    private bool hasShield = false;

    void Start()
    {
        powerUps = new Queue<string>();
    }
    private GameObject powerBall = null;
    private float powerBallTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (powerBall && powerBallTimer < Time.time)
        {
            DestroyShield();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UsePowerUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RatObstacle"))
        {
            if (hasShield) DestroyShield();
            else TakeDamage(40);
            Destroy(other.gameObject);
        }
        else powerUps.Enqueue(other.name);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }

    private void UsePowerUp()
    {
        if (powerUps.Count > 0 && coolDown < Time.time)
        {
            string powerUpName = powerUps.Dequeue();
            coolDown = Time.time + coolDownTime;

            if (powerUpName == "HamsterBall")
            {
                powerBall = Instantiate(hamsterBall);
                Destroy(powerBall.GetComponent("Box Collider"));
                powerBall.transform.parent = transform;
                powerBall.transform.localPosition = Vector3.zero;
                powerBallTimer = Time.time + coolDownTime;
                hasShield = true;
            }
            Debug.Log("Power UP used");
        }
    }

    private void DestroyShield()
    {
        Destroy(powerBall);
        powerBall = null;
        hasShield = false;
    }
}

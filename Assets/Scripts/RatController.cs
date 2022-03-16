using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatController : MonoBehaviour
{
    public float coolDownTime = 3;
    public GameObject hamsterBall;
    public Slider m_RatHealth;
    // Start is called before the first frame update

    Queue<string> powerUps;

    private int health = 100;
    private float coolDown = 0;
    private bool hasShield = false;
    private bool isBig = false;

    void Start()
    {
        powerUps = new Queue<string>();
        m_RatHealth.value = health;
    }
    private GameObject powerBall = null;
    private float powerBallTimer = 0;
    private float effectTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (powerBall && powerBallTimer < Time.time)
        {
            DestroyShield();
        }

        if (isBig && effectTimer < Time.time)
        {
            isBig = false;
            transform.localScale *= 0.2f;
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
        else if (other.CompareTag("ScientistObstacle"))
        {
            powerUps.Enqueue(other.name);
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        m_RatHealth.value = health * 0.01f;
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

            if (powerUpName == "Mushroom")
            {
                transform.localScale *= 5;
                isBig = true;
                effectTimer = Time.time + 3;
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

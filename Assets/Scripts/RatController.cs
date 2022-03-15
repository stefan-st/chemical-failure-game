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

    void Start()
    {
        powerUps = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UsePowerUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RatObstacle"))
        {
            TakeDamage(10);
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
                GameObject powerBall = Instantiate(hamsterBall);
                Destroy(powerBall.GetComponent("Box Collider"));
                powerBall.transform.parent = transform;
                powerBall.transform.localPosition = Vector3.zero;
            }
            Debug.Log("Power UP used");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScientistController : MonoBehaviour
{
    public float coolDownTime = 3;
    public GameObject mouseTrap;
    public Slider m_ScientistHealth;
    // Start is called before the first frame update

    Queue<string> powerUps;

    private int health = 100;
    private float coolDown = 0;
    private bool hasShield = false;

    private GameObject trap = null;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = new Queue<string>();
        m_ScientistHealth.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            UsePowerUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RatObstacle"))
        {
            powerUps.Enqueue(other.name);
        }
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

            if (powerUpName == "RatTrap")
            {
                trap = Instantiate(mouseTrap);
                mouseTrap.transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
                mouseTrap.tag = "RatObstacle";
                Debug.Log("Power UP used");
            }
        }
    }
}

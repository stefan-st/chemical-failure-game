using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour
{
    public GameObject destroyedComputer;
    public Slider m_slider;

    private float destroyProgress = 0;
    // Start is called before the first frame update

    private bool isDestroyed;
    private bool isActive;
    void Start()
    {
        isDestroyed = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyProgress > 0)
        {
            destroyProgress -= 0.001f * Time.time;
        }
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                destroyProgress += 0.04f * Time.time;
            }
        }

        m_slider.value = destroyProgress;
        if (destroyProgress > 1) DestroyComputer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rat"))
        {
            isActive = true;
            destroyProgress = 0;
            //DestroyComputer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rat"))
        {
            destroyProgress = 0;
            isActive = false;
        }
    }

    private void FixComputer()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void DestroyComputer()
    {
        GameObject o = Instantiate(destroyedComputer);
        o.transform.localPosition = transform.localPosition;

        Destroy(gameObject);
        isDestroyed = true;
    }
}

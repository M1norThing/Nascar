using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarBehavior : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float speedGainByTime = 0.2f;
    [SerializeField] float steerSpeed = 200f;

    int steerValue;

    void Update()
    {
        speed += speedGainByTime * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(0f, steerValue * steerSpeed * Time.deltaTime, 0f);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}

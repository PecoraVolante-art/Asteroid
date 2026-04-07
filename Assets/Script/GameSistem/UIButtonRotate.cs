using UnityEngine;

public class UIButtonRotate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public float turnSpeed = 60f;
    void Start()
    {

    }


    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}



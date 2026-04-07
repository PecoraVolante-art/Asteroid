using UnityEngine;

public class SceWrapper : MonoBehaviour
{

    private float leftCostrain;
    private float rightCostrain;
    private float topCostrain;
    private float bottomCostrain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float distanceZ = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        leftCostrain = Camera.main.ScreenToWorldPoint(new Vector3(0,0, distanceZ)).x;
        rightCostrain = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width -1 , 0, distanceZ)).x;
        topCostrain =  Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).y;
        bottomCostrain = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftCostrain)
        {
            transform.position = new Vector3(rightCostrain, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightCostrain)
        { 
        transform .position = new Vector3(leftCostrain,transform.position.y, transform.position.z);
        }
        if (transform.position.y > topCostrain)
        {
            transform.position = new Vector3(transform.position.x, bottomCostrain, transform.position.z);
        }

        if (transform.position.y < bottomCostrain)
        {
            transform.position = new Vector3(transform.position.x, topCostrain, transform.position.z);
        }
    }
}

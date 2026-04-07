using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private Rigidbody2D rb;
    private float forwardInput;
    private float turnInput;
    public float forwardSpeed;
    public float turnSpeed;


    public GameObject bulletPrefab; 

    public bool justhit;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update viene chiamato ogni frame: QUI leggiamo l'input del giocatore
    void Update()
    {
     
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            TeleportRandomly();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);

            if (GestioneSFX.Instance != null)
                GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.shoot);
        }
    }


   
   
    void FixedUpdate()
    {
       
        if (turnInput != 0)
        {
    
            float rotationAmount = -turnInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation + rotationAmount);
        }


        if (forwardInput > 0)
        {

            rb.AddForce(transform.up * forwardSpeed * forwardInput);
        }

    }

   
    void TeleportRandomly()
    {
       
        float distanceZ = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        Vector2 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ));
        Vector2 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, distanceZ));

     
        float randomX = Random.Range(screenBottomLeft.x + 1f, screenTopRight.x - 1f);
        float randomY = Random.Range(screenBottomLeft.y + 1f, screenTopRight.y - 1f);

      
        transform.position = new Vector3(randomX, randomY, 0);

     }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Just Hit");
            GameManager.playerLives = GameManager.playerLives - 1;
            Debug.Log(GameManager.playerLives);
            transform.position = Vector3.zero;
            justhit = true;
            StartCoroutine(NoHitTimer());
            if (GestioneSFX.Instance != null)
                GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.death);
        }
        CheckEndGame();
    }



    IEnumerator NoHitTimer()
    {
        for (float i = 2f; i >= 0; i -= 0.25f)
        {
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 25);
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 25);
            yield return new WaitForSeconds(0.1f);

        }
    
    
    justhit = false;
    yield return null;
    }

    private void CheckEndGame()
    {
        if (GameManager.playerLives <= 0)
        {
            Debug.Log("GameOver");
        }
    }
}
using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float maxAngularSpeed;

    public enum Type {Big,Medium,Small };
    public Type type;

    private int bigPoints = 100;
    private int smallPoints = 25;
    private int mediumPoints = 50;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle.normalized * speed);
        rb.angularVelocity = Random.Range(-maxAngularSpeed, maxAngularSpeed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (type == Type.Big)
            {
                GameManager.Instance.SpawnAsteroid(transform.position, Type.Medium);
                GameManager.Instance.SpawnAsteroid((transform.position + new Vector3(1, 0.4f, transform.position.z)), Type.Medium);
                GameManager.playerscore = GameManager.playerscore + bigPoints;
                GameManager.Instance.numCurrentBigAsteroids--;
                if (GestioneSFX.Instance != null)
                    GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.destroy);
            }
            else if (type == Type.Medium)
            {
                GameManager.Instance.SpawnAsteroid(transform.position, Type.Small);
                GameManager.Instance.SpawnAsteroid((transform.position + new Vector3(1, 0.4f, transform.position.z)), Type.Small);
                GameManager.playerscore = GameManager.playerscore + mediumPoints;
                if (GestioneSFX.Instance != null)
                    GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.destroy);

            }
            else if (type == Type.Small)
            {
              GameManager.playerscore = GameManager.playerscore + smallPoints;
                if (GestioneSFX.Instance != null)
                    GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.destroy);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);

            GameManager.Instance.numCurrentAsteroids--;

        }
    }
}

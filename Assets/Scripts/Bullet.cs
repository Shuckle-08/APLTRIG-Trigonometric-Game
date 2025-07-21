using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, 3f); // Bullet disappears after 3 seconds
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Add score
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(1);
            }
        }
    }
}

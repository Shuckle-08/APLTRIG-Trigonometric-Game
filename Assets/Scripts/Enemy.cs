using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Rotate toward the center
        Vector3 directionToCenter = -transform.position.normalized;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Check if it reached the center
        if (transform.position.magnitude < 0.2f)
        {
            if (LifeManager.Instance != null)
            {
                LifeManager.Instance.LoseLife();
            }
            

            Destroy(gameObject);
        }
    }
}

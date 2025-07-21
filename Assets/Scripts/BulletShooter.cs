using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;

    /// <summary>
    /// Shoots a bullet in the specified direction.
    /// </summary>
    public void ShootBullet(Vector2 direction)
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }

        // Instantiate bullet at the center
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    // These public methods are called by the UI Buttons

    public void ShootUp()
    {
        ShootBullet(Vector2.up);
    }

    public void ShootDown()
    {
        ShootBullet(Vector2.down);
    }

    public void ShootLeft()
    {
        ShootBullet(Vector2.left);
    }

    public void ShootRight()
    {
        ShootBullet(Vector2.right);
    }

    public void ShootTopRight()
    {
        ShootBullet(new Vector2(1, 1));
    }

    public void ShootTopLeft()
    {
        ShootBullet(new Vector2(-1, 1));
    }

    public void ShootBottomRight()
    {
        ShootBullet(new Vector2(1, -1));
    }

    public void ShootBottomLeft()
    {
        ShootBullet(new Vector2(-1, -1));
    }
}

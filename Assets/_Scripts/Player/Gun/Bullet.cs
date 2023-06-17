using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("Build"))
        {
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [Header("Params")]
    [Range(50, 500)]
    [SerializeField]
    private int _damage = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().DamageHealth(_damage);
        }
    }
}

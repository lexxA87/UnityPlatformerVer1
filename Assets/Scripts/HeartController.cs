using UnityEngine;

public class HeartController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    private int _count = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().AddHealth(_count);
            Destroy(gameObject);
        }
    }
}

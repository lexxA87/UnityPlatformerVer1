using UnityEngine;

public class HeartController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    private int _count = 50;
    [SerializeField] private AudioClip _pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(_pickupSound);
            collision.GetComponent<PlayerHealth>().AddHealth(_count);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class CoinController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    private int _count = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerScore>().AddCoin(_count);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

    private GameObject _player;
    private Rigidbody2D _body2D;

    [Range(0.1f, 10f)]
    [SerializeField] private float _force;

    // Start is called before the first frame update
    void Start()
    {
        _body2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = _player.transform.position - transform.position;
        _body2D.velocity = new Vector2(direction.x, direction.y).normalized * _force;

        float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -rot - 90);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

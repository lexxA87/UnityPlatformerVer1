using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    [SerializeField] private GameObject _spawnThingPrefab;
    [Range(1f, 10f)]
    [SerializeField] private float _spawnCooldown = 5;

    private float _timer;

    private void Update()
    {
        int children = transform.childCount;

        _timer += Time.deltaTime;

        if (_timer >= _spawnCooldown)
        {
            if (children > 0) return;

            Instantiate(_spawnThingPrefab, transform);

            _timer = 0;
        }
    }
}

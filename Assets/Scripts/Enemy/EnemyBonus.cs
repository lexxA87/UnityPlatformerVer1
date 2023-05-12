using UnityEngine;

public class EnemyBonus : MonoBehaviour
{
    [SerializeField] private GameObject _bonusPrefab;

    public void CreateBonus()
    {
        Instantiate(_bonusPrefab, transform.position, Quaternion.identity);
    }
}

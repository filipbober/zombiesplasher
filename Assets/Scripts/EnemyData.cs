using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour
{
    public Enums.EnemyType EnemyType { get { return _enemyType; } }
    public int MaxHealth { get { return _maxHealth; } }
    public float Speed { get { return _speed; } }
    public float SpeedFluctuation { get { return _speedFluctuation; } }

    [SerializeField]
    private Enums.EnemyType _enemyType;

    [SerializeField]
    private int _maxHealth;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _speedFluctuation;

    private int _currentHealth;

    void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    //public void Initialize(Enums.EnemyType enemyType, int health, float speed)
    //{
    //    _enemyType = enemyType;
    //    _maxHealth = health;
    //    _speed = speed;

    //    _currentHealth = _maxHealth;
    //}

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public bool IsDead()
    {
        if (_currentHealth <= 0)
            return true;
        else
            return false;
    }

}

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private float _lifetime;
    private bool _expired;

    [SerializeField] private float _ltMin = 10f;
    [SerializeField] private float _ltMax = 20f;

    public event Action<Cube> Expired;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Begin()
    {
        _lifetime = UnityEngine.Random.Range(_ltMin, _ltMax);
        _expired = false;
    }

    void Update()
    {
        if (_expired) return;

        LifetimeCountdown();
        if (_lifetime <= 0f)
        {
            _expired = true;
            Expired?.Invoke(this);
        }
    }

    private void LifetimeCountdown()
    {
        if (_lifetime >= 0f)
        {
            _lifetime -= Time.deltaTime;
        }
    }

}

    
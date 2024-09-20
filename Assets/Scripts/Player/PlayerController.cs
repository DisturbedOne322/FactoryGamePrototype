using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField, Min(1)]
    private float _moveSpeed;

    private Vector3 _moveDir;

    private ICharacterInputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = GetComponent<ICharacterInputProvider>();
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var input = _inputProvider.GetMoveInput().normalized;
        _moveDir = new Vector3(input.x, 0 ,input.y);

        if (!ValidInput())
            return;
        transform.forward = _moveDir;
    }

    private void FixedUpdate()
    {
        if (!ValidInput())
            return;
        _rb.velocity = _moveDir * _moveSpeed;
    }

    private bool ValidInput() => _moveDir.x != 0 || _moveDir.z != 0;
}

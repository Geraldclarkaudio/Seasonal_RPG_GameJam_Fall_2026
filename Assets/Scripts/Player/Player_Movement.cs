using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]
    InputManager _input;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float groundDistance = 0.3f;

    Rigidbody _rb;
    [SerializeField]
    private float _gravity;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    void Update()
    {
        Vector2 move = _input.Move;

        transform.Translate(new Vector3(move.x, 0, 0) * _speed * Time.deltaTime); // left and right inputs only effect horizontal movement.   
    }

    private void FixedUpdate()
    {
        //check if grounded.. 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, _groundLayer);

        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * (_gravity - 1) * Time.fixedDeltaTime;
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}

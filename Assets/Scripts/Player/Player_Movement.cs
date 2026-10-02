using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]
    InputManager _input;

    [SerializeField]
    private float _speed;

    void Update()
    {
        Vector2 move = _input.Move;

        transform.Translate(new Vector3(move.x, 0, 0) * _speed * Time.deltaTime); // left and right inputs only effect horizontal movement. 
    }
}

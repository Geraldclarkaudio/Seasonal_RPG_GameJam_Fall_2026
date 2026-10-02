using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputSystem_Actions _inputSystem;

    public Vector2 Move => _inputSystem.Player.Move.ReadValue<Vector2>(); // made movement a property that is only accessed when used. 


    void OnEnable() // set up three basic button type inputs for now. Player movement is polled in the player movement class
    {
        _inputSystem = new();
        _inputSystem.Player.Enable();
        _inputSystem.Player.Attack.performed += Attack_performed;
        _inputSystem.Player.Jump.performed += Jump_performed;
        _inputSystem.Player.Interact.performed += Interact_performed;

    }

    private void Start()
    {

    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Interact");
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Attack");
    }

}

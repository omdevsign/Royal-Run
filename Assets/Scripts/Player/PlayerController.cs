using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 2f;
    Vector2 movement;
    Rigidbody rigidBody;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log("Movement Input: " + movement);
    }
    void HandleMovement()
    {
        Vector3 currentPosition = rigidBody.position;
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);
        
        rigidBody.MovePosition(newPosition);
    }
}

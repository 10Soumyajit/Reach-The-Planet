using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 50f;
    Rigidbody rb;

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
       }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float rotateThisFrame)
    {
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

}

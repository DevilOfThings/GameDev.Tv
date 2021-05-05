using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private float acceleration = 100f;
    [SerializeField]
    private float rotationSpeed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    Vector3 calcRotation => Vector3.forward*rotationSpeed*Time.deltaTime;
    Vector3 calcRelativeForce => Vector3.up*acceleration*Time.deltaTime;
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(calcRotation);
        }
        else if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-calcRotation);
        }        
    }

    void ApplyRotation(Vector3 calculatedRotation)
    {
        rb.freezeRotation = true;            
        transform.Rotate(calculatedRotation);
        rb.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.UpArrow))
        {            
            Debug.Log("Thrust");
            rb.AddRelativeForce(calcRelativeForce);
        }

    }
}

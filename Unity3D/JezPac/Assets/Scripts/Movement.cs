using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    [SerializeField]
    private float acceleration = 100f;
    [SerializeField]
    private float rotationSpeed = 25f;

    [SerializeField]
    AudioClip ThrusterAudio;

    [SerializeField]
    ParticleSystem thrusterParticles;
    [SerializeField]
    ParticleSystem[] boosterParticles;
    [SerializeField]
    private float maxY = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();        
    }

    Vector3 calcRotation => Vector3.forward*rotationSpeed*Time.deltaTime;
    Vector3 calcRelativeForce => Vector3.up*acceleration*Time.deltaTime;

    bool IsThrottleOpen =>Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.UpArrow);
    bool IsSteeringLeft => Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow);
    bool IsSteeringRight => Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow);
    void ProcessRotation()
    {
        boosterParticles.ToList().ForEach(x=>x.Stop());

        if(IsSteeringLeft)
        {
            ApplyRotation(calcRotation);            
        }
        else if(IsSteeringRight)
        {
            ApplyRotation(-calcRotation);
        }        
    }
    

    void ApplyRotation(Vector3 calculatedRotation)
    {
        rb.freezeRotation = true;            
        transform.Rotate(calculatedRotation);

        if(calculatedRotation.y > 0) 
        {
            boosterParticles[0].Play();
            boosterParticles[1].Play();   
        }
        else 
        {            
            boosterParticles[2].Play();
            boosterParticles[3].Play();
        }
        rb.freezeRotation = false;
    }

    
    void ProcessThrust()
    {   
        if(IsThrottleOpen) 
        {
            StartThrusting();
         } 
         else 
         {
            StopThrusting();
         } 
    }

    private void StopThrusting()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (thrusterParticles.isPlaying)
        {
            thrusterParticles.Stop();
        }
    }

    private void StartThrusting()
    {
        if(rb.position.y < maxY)
        {
            rb.AddRelativeForce(calcRelativeForce);
        }
        

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(ThrusterAudio);
        }
        if (!thrusterParticles.isPlaying)
        {
            thrusterParticles.Play();
        }
    }
}

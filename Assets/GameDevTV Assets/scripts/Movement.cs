using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
     [SerializeField] float thruststrenght;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip mainengine;
    [SerializeField] ParticleSystem enginethrustparticle;
    [SerializeField] ParticleSystem rightenginethrustparticle;
    [SerializeField] ParticleSystem leftenginethrustparticle;

    Rigidbody rb;
    AudioSource audioSource;
   

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() 
    {
        thrust.Enable();
        rotation.Enable();
        
    }

      private void FixedUpdate()
    {
        processthrust();
        processrotation();
    }
    private void processthrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thruststrenght * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainengine);
            }
            if(!enginethrustparticle.isPlaying)
            {
             enginethrustparticle.Play();
            }

           
        }
        else
        {
            audioSource.Stop();
            enginethrustparticle.Stop();
        }
    }

    private void processrotation () 
    {
        float rotationvalue = rotation.ReadValue<float>();
        
        if (rotationvalue < 0)
        {

            applyrotation(rotationSpeed);
             if(!rightenginethrustparticle.isPlaying)
            {
              leftenginethrustparticle.Stop();
              rightenginethrustparticle.Play();
            }

        }


        else if(rotationvalue > 0) 
         {
           applyrotation(-rotationSpeed);
           if(!leftenginethrustparticle.isPlaying)
           {
            rightenginethrustparticle.Stop();
            leftenginethrustparticle.Play();
           }
         }
         else
         {
            rightenginethrustparticle.Stop();
            leftenginethrustparticle.Stop();
         }
    }

    private void applyrotation(float Rotationthisframe)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Rotationthisframe * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Collisionhandler : MonoBehaviour
{
    [SerializeField] float leveldelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip levelComplete;
    AudioSource audioSource;
    [SerializeField]ParticleSystem crashparticle;
    [SerializeField]ParticleSystem levelCompleteParticle;
    bool iscontrollable = true;

   void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        
    }
      private void OnCollisionEnter(Collision other) 
   {
    
            if(!iscontrollable)
            {return;}

     switch(other.gameObject.tag )
        {
            case "friendlyobject":
            Debug.Log("Friendly object collided");
            break;

            case "Finish":
            SuccessSequence();
         
            break;

            default :
            crashSequence();
            
              break;

        }
   }

    private void SuccessSequence()
    {
        
        iscontrollable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(levelComplete);
        levelCompleteParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke( "Loadnextscene", leveldelay);
    }

    void crashSequence()
    {
        
         iscontrollable = false;
         audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashparticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", leveldelay);
    }

    void Loadnextscene()
            {
                 int sceneloader = SceneManager.GetActiveScene().buildIndex;
                 int nextscene = sceneloader + 1;
                 if(nextscene == SceneManager.sceneCountInBuildSettings)
                 {
                    Debug.Log("You have completed the game");
                    return;
                 }
                SceneManager.LoadScene(nextscene);
            }

               void ReloadScene()
              {
                int sceneloader = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneloader);
              }

              private void Update() 
              {
                Respondtodebugkeys();
              }

    private void Respondtodebugkeys()
    {
        if(Keyboard.current.lKey.isPressed)
        {
            Loadnextscene();
        }
    }
}

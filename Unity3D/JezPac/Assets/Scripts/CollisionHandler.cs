using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float delayInSeconds = 1f;    

    [SerializeField]
    private AudioClip successAudio;
    [SerializeField]
    private AudioClip failedAudio;

    private AudioSource audioSource;

    [SerializeField]
    private ParticleSystem successParticles;
    [SerializeField]
    private ParticleSystem failedParticles;

    [SerializeField]
    public bool CollisionsDisabled = false;
    

    private bool isTransitioning { get; set; } = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning)
        {
            return;
        }

        switch(other.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("Fuel");
            break;
            case "Finish":
                Debug.Log("Finish");
                SuccessSequence();
            break;
            case "Friendly":
                Debug.Log("Friendly");
            break;
            default:
                Debug.Log($"Bang!!! {other.gameObject.name} {other.gameObject.tag}");
                CrashSequence();
            break;
        }
    }

    private void CrashSequence()
    {
        if(CollisionsDisabled)
        {
            GetComponent<Movement>().enabled = true;
            return;
        }
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;     
        audioSource.PlayOneShot(failedAudio);
        failedParticles.Play();
        Invoke("ReloadLevel", delayInSeconds);
    }

    private void ReloadLevel()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);        
    }

    private void SuccessSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        Invoke("LoadNextLevel", delayInSeconds);
    }

    public void LoadNextLevel()
    {        
        var activeScene = SceneManager.GetActiveScene().buildIndex;
        activeScene = ++activeScene == SceneManager.sceneCountInBuildSettings ? 0 : activeScene;
        SceneManager.LoadScene(activeScene);
    }

}

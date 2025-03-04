using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) {
        if (isTransitioning) { return; }
        string tag = other.gameObject.tag;
        switch (tag) {
            case "Friendly":
                Debug.Log("Friendly object collided");
                break;
            case "Finish":
                Debug.Log("You Finished");
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        //todo add particle effect on finish
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", LevelLoadDelay);
    }
    void StartCrashSequence()
    {
        //todo add particle effect on crash
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("Reloadlevel", LevelLoadDelay);
    }
    
    void Reloadlevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

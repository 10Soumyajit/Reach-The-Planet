using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        string tag = other.gameObject.tag;
        switch (tag) {
            case "Friendly":
                Debug.Log("Friendly object collided");
                break;
            case "Fuel":
                Debug.Log("Picking Fuel");
                break;
            case "Finish":
                Debug.Log("You Finished");
                NextLevel();
                break;
            default:
                Reloadlevel();
                break;
        }
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

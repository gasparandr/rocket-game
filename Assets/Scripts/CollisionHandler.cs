using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelay = 1f;
  void OnCollisionEnter(Collision other)
  {
    Debug.Log($"Collision object tag {other.gameObject.tag}");
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Bumped into friendly.");
        break;

      case "Finish":
        StartSuccessSequence();
        break;

      default:
        StartCrashSequence();
        break;
    }
  }

  void StartSuccessSequence()
  {
    // TODO: Add SFX upon success.
    // TODO: Add particle effect upon success.
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  void StartCrashSequence()
  {
    // TODO: Add SFX upon crash.
    // TODO: Add particle effect upon crash.
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", 1f);
  }

  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }
}

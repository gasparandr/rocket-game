using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelay = 1f;
  [SerializeField] AudioClip successSound;
  [SerializeField] AudioClip crashSound;
  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;

  AudioSource audioSource;

  bool isTransitioning = false;
  bool collisionDisabled = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    RespondToDebugKeys();
  }

  void RespondToDebugKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      collisionDisabled = !collisionDisabled;
    }
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransitioning || collisionDisabled)
    {
      return;
    }

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
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(successSound);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  void StartCrashSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(crashSound);
    crashParticles.Play();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

  [SerializeField] float mainThrust = 100f;
  [SerializeField] float rotationThrust = 100f;
  [SerializeField] AudioClip mainEngine;
  [SerializeField] ParticleSystem mainEngineParticles;
  [SerializeField] ParticleSystem leftThrusterParticles;
  [SerializeField] ParticleSystem rightThrusterParticles;

  Rigidbody rb;
  AudioSource audioSource;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      StartThrusting();
    }
    else
    {
      StopThrusting();
    }

  }

  void StartThrusting()
  {
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(mainEngine);
    }
    if (!mainEngineParticles.isPlaying)
    {
      mainEngineParticles.Play();
    }
  }

  void StopThrusting()
  {
    audioSource.Stop();
    mainEngineParticles.Stop();
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      StartRotateLeft();
    }
    else
    {
      StopRotateLeft();
    }

    if (Input.GetKey(KeyCode.D))
    {
      StartRotateRight();
    }
    else
    {
      StopRotateRight();
    }

    void ApplyRotation(float rotationThisFrame)
    {
      rb.freezeRotation = true; // Freezing rotation so we can manually rotate
      transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
      rb.freezeRotation = false; // Unfreeze rotation
    }

    void StartRotateLeft()
    {
      ApplyRotation(rotationThrust);

      if (!rightThrusterParticles.isPlaying)
      {
        rightThrusterParticles.Play();
      }
    }

    void StopRotateLeft()
    {
      if (rightThrusterParticles.isPlaying)
      {
        rightThrusterParticles.Stop();
      }
    }

    void StartRotateRight()
    {
      ApplyRotation(-rotationThrust);
      if (!leftThrusterParticles.isPlaying)
      {
        leftThrusterParticles.Play();
      }
    }

    void StopRotateRight()
    {
      if (leftThrusterParticles.isPlaying)
      {
        leftThrusterParticles.Stop();
      }
    }
  }
}

using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
  void OnCollisionEnter(Collision other)
  {
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Bumped into friendly.");
        break;

      case "Fuel":
        Debug.Log("Bumped into fuel");
        break;

      case "Finish":
        Debug.Log("Finished successfully!");
        break;

      default:
        Debug.Log("Could not find tag on collision object.");
        break;
    }
  }
}

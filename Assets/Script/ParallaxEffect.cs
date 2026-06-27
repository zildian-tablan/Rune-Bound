using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget,followTarget2,followTarget3,followTarget4;

    //starting for parallax object
    Vector2 startPos;

    //start of parallax z
    float startingZ;

    //=> mean update every frame
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startPos;
    float zdistanceFromTarget => transform.position.z - followTarget.position.z;
  

    float clippingPlane => (cam.transform.position.z + (zdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
  
    float parallaxFactor => Mathf.Abs(zdistanceFromTarget / clippingPlane);
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
  
            Vector2 newPos = startPos + camMoveSinceStart * parallaxFactor;
            transform.position = new Vector3(newPos.x, newPos.y, startingZ);
       
    }
}

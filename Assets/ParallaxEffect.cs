using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxCoeficient = 1;
    [SerializeField] private List<GameObject> backgroundLayers;
    private Vector3 lastScreenPosition;
    private Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();
        lastScreenPosition = transform.position;
    }

    private void LateUpdate() 
    {
        foreach (var layer in backgroundLayers)
        {
            float distanceToCamera = (layer.transform.position.z - cam.transform.position.z);
            
            float screenSpeed = (transform.position.x - lastScreenPosition.x);

            float parallaxSpeed = -1f * screenSpeed * parallaxCoeficient / distanceToCamera;

            layer.transform.Translate(Vector3.right * parallaxSpeed, Space.World);
        }

        lastScreenPosition = transform.position;
    }
}

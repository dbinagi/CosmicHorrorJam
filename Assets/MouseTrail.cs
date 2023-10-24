using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrail : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;

    public float distanceThreshold = 0.1f; // Distancia mínima para actualizar el trail
    public float mousePositionZ;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Obtén la posición del mouse en el mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mousePositionZ; // Ajusta la profundidad para que esté en el mismo plano que el objeto

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Actualiza la posición del objeto con el Trail Renderer
        if (Vector3.Distance(worldPosition, transform.position) > distanceThreshold)
        {
            transform.position = worldPosition;
        }

        trail.transform.position = this.transform.position;
    }
}

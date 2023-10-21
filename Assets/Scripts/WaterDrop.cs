using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    Rigidbody body;
    public float velocidadY = 3.0f; // Velocidad de caída
    public float desviacionX = 0.1f; // Máxima desviación en el eje X
    private Vector3 posicionInicial;

    float desviacionAleatoria;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        posicionInicial = transform.position;

        // Calcular una desviación aleatoria en X
        desviacionAleatoria = Random.Range(-desviacionX, desviacionX);
    }

    // Update is called once per frame
    void Update()
    {

        // Calcular la nueva posición
        Vector3 nuevaPosicion = new Vector3(
            posicionInicial.x,
            transform.position.y - velocidadY * Time.deltaTime,
            transform.position.z + desviacionAleatoria
        );

        // Actualizar la posición del objeto
        transform.position = nuevaPosicion;

        // Reiniciar la gota si sale de la vista de la cámara
        if (nuevaPosicion.y < -Camera.main.orthographicSize)
        {
            Destroy(this.gameObject);
            //     ReiniciarGota();
        }
    }

    void ReiniciarGota()
    {
        // Restablecer la posición inicial y generar una nueva desviación aleatoria
        transform.position = posicionInicial;
    }
}

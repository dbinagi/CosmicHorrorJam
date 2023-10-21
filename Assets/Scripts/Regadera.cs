using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regadera : MonoBehaviour
{
    // private Vector3 offset;
    bool dragging;
    // private ParticleSystem particleSystem; // Referencia al sistema de partículas

    float lastTimeDrop;


    [SerializeField]
    float dropCD;

    [SerializeField]
    GameObject waterDropPos;

    void Start()
    {
        // Busca el sistema de partículas en uno de los hijos
        // particleSystem = GetComponentInChildren<ParticleSystem>();
        // if (particleSystem != null)
        // {
        //     particleSystem.Stop(); // Asegura que el sistema de partículas comience detenido
        // }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit? hit = GetMouseRaycastHit();

            if (hit.HasValue && !dragging)
            {
                // Debug.Log(hit.Value.transform.name);
                if (hit.Value.transform.gameObject == this.gameObject)
                {
                    OnDown();
                }
            }

            //     offset = transform.position - GetMouseWorldPosition();
            //     dragging = true;
            //
            //     if (particleSystem != null)
            //     {
            //         particleSystem.Play(); // Inicia el sistema de partículas cuando se inicia el arrastre
            //     }
            // }
            // else if (Input.GetMouseButtonUp(0))
            // {
            //     dragging = false;
            //
            //     if (particleSystem != null)
            //     {
            //         particleSystem.Stop(); // Detiene el sistema de partículas cuando se detiene el arrastre
            //     }
            // }
            //
            // if (dragging)
            // {
            //     Vector3 targetPosition = GetMouseWorldPosition() + offset;
            //     transform.position = targetPosition;
            // }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (dragging)
                OnUp();
        }

        if (dragging)
        {
            // Obtén la posición del mouse en el mundo
            Vector3 posicionMouseEnMundo = GetMouseWorldPosition();

            // Actualiza la posición del objeto para que siga la posición del mouse en el mundo
            // transform.position = new Vector3(this.transform.position.x, posicionMouseEnMundo.y, posicionMouseEnMundo.z);
            transform.position = posicionMouseEnMundo;


            if (Time.time - lastTimeDrop >= dropCD)
            {
                lastTimeDrop = Time.time;
                StartCoroutine(DropWater());
            }

        }

        IEnumerator DropWater()
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = waterDropPos.transform.position + ((Vector3.forward * i) * 0.02f) * -0.8f;
                Instantiate(Resources.Load("WaterDrop"), pos, Quaternion.identity);
            }
            yield return new WaitForEndOfFrame();
        }

        // private Vector3 GetMouseWorldPosition()
        // {
        //     Vector3 mousePosition = Input.mousePosition;
        //     mousePosition.z = -Camera.main.transform.position.z;
        //     return Camera.main.ScreenToWorldPoint(mousePosition);
        // }

    }
    private Vector3 GetMouseWorldPosition()
    {
        // Obtiene la posición del mouse en pantalla y la convierte en coordenadas del mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z * 2.5f;
        // mousePosition.z = -Camera.main.transform.position.z;
        // mousePosition.x = transform.position.x;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnDown()
    {
        // particleSystem.Play();
        dragging = true;
    }

    void OnUp()
    {
        // particleSystem.Stop();
        dragging = false;
    }

    RaycastHit? GetMouseRaycastHit()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        LayerMask mask = LayerMask.GetMask("Interactable");

        if (Physics.Raycast(ray, out RaycastHit hitData, 100.0f, mask))
        {
            return hitData;
        }
        return null;
    }


}

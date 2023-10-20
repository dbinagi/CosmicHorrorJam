using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSlot : MonoBehaviour
{

    [SerializeField]
    GameObject poopImage;

    public bool isEmpty;

    // Start is called before the first frame update
    void Start()
    {
        poopImage.SetActive(false);
        isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Poop()
    {
        poopImage.SetActive(true);
        isEmpty = false;
    }

    public void TakePoop()
    {
        isEmpty = true;
        poopImage.SetActive(false);
    }
}

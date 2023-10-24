using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{

    public GameObject signHorizontal;
    public GameObject signVertical;
    public GameObject signTriangle;
    public GameObject signU;
    public GameObject signLess;
    public GameObject signHappy;
    public GameObject signSad;
    public GameObject signSquare;

    List<GameObject> randomSigns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {

        signHorizontal.SetActive(false);
        signVertical.SetActive(false);
        signTriangle.SetActive(false);
        signU.SetActive(false);
        signLess.SetActive(false);
        signHappy.SetActive(false);
        signSad.SetActive(false);
        signSquare.SetActive(false);

        randomSigns.Add(signTriangle);
        randomSigns.Add(signHorizontal);
        randomSigns.Add(signVertical);
        randomSigns.Add(signU);
        randomSigns.Add(signSquare);
    }

    public int ShowRandomSign()
    {
        int randomI = Random.Range(0, randomSigns.Count - 1);
        foreach (GameObject o in randomSigns)
        {
            o.SetActive(false);
        }
        randomSigns[randomI].SetActive(true);
        return randomI;
    }


    // Método para mezclar aleatoriamente una lista
    private void RandomizeList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int j = Random.Range(i, n);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

}

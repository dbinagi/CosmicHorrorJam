using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{

    [SerializeField]
    Slider waterSlider;

    public int currentStage;

    public const int STAGE_TO_PLANT = 0;
    public const int STAGE_PLANTED = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentStage = STAGE_TO_PLANT;
        waterSlider.maxValue = GameManager.Instance.balance.maxWaterForPlant;
    }

    // Update is called once per frame
    void Update()
    {
        if (waterSlider.value <= 0)
        {
            SetStage(STAGE_TO_PLANT);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<WaterDrop>() != null)
        {
            waterSlider.value += GameManager.Instance.balance.waterPerDrop;
        }
    }

    void SetStage(int stage)
    {
        currentStage = stage;
        if (currentStage == STAGE_TO_PLANT)
        {
            waterSlider.gameObject.SetActive(false);
        }
        else if (currentStage == STAGE_PLANTED)
        {
            waterSlider.gameObject.SetActive(true);
        }
    }

    public void PutPlant()
    {
        if (currentStage == STAGE_TO_PLANT)
        {
            SetStage(STAGE_PLANTED);
        }
    }
}

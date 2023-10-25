using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{

    [SerializeField]
    Slider waterSlider;

    [SerializeField]
    GameObject waitIndicator;

    public int currentStage;

    public const int STAGE_PLANTED = 0;
    public const int STAGE_READY = 1;
    public const int STAGE_WAIT = 2;

    [SerializeField]
    public GameObject plantStage0;

    [SerializeField]
    public GameObject plantStage1;

    float timeSetWaited;

    void Start()
    {
        waterSlider.maxValue = GameManager.Instance.balance.maxWaterForPlant;
        waterSlider.value = 0;
        SetStage(STAGE_PLANTED);
    }

    void Update()
    {
        if (currentStage == STAGE_PLANTED)
        {
            if (waterSlider.value >= waterSlider.maxValue)
            {
                SetStage(STAGE_WAIT);
            }
        }

        if (GameManager.Instance.gameStarted && currentStage == STAGE_WAIT)
        {
            if (Time.time - timeSetWaited >= GameManager.Instance.balance.plantTimeToGrab)
            {
                SetStage(STAGE_READY);
            }
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
        if (currentStage == STAGE_PLANTED)
        {
            waterSlider.gameObject.SetActive(true);
            waterSlider.value = 0;
            plantStage0.SetActive(true);
            plantStage1.SetActive(false);
            waitIndicator.SetActive(false);
        }
        else if (currentStage == STAGE_READY)
        {
            waterSlider.gameObject.SetActive(false);
            plantStage0.SetActive(false);
            plantStage1.SetActive(true);
            waitIndicator.SetActive(false);
        }
        else if (currentStage == STAGE_WAIT)
        {
            waterSlider.gameObject.SetActive(false);
            timeSetWaited = Time.time;
            waitIndicator.SetActive(true);
        }
    }

    public void Take()
    {

        SetStage(STAGE_PLANTED);
    }

}

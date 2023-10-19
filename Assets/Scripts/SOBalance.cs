using UnityEngine;

[CreateAssetMenu(fileName = "Balance", menuName = "ScriptableObjects/Balance", order = 1)]
public class SOBalance : ScriptableObject
{
    public float hourDuration;

    public int daysForEnd;
    public int petMaxHunger;
    [Header("Level 0")]
    public float hungerMaxCooldownLevel0;
    public float hungerMinCooldownLevel0;

    public int hungerLossMinLevel0;
    public int hungerLossMaxLevel0;

    [Header("Level 1")]

    public int daysForLevel1;
    public float hungerMaxCooldownLevel1;
    public float hungerMinCooldownLevel1;

    public int hungerLossMinLevel1;
    public int hungerLossMaxLevel1;

    [Header("Level 2")]

    public int daysForLevel2;
    public float hungerMaxCooldownLevel2;
    public float hungerMinCooldownLevel2;

    public int hungerLossMinLevel2;
    public int hungerLossMaxLevel2;

    [Header("Food")]
    public float foodRatValue;
    public float foodVegetablesValue;
    public float foodHumanValue;

    [Header("Rat")]
    public float RatSpawnCooldown;
    public float RatSpeedLevel0;
    public float RatSpeedLevel1;
    public float RatSpeedLevel2;

}

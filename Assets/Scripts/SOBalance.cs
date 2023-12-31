using UnityEngine;

[CreateAssetMenu(fileName = "Balance", menuName = "ScriptableObjects/Balance", order = 1)]
public class SOBalance : ScriptableObject
{
    public float hourDuration;

    public int daysForEnd;
    public int petMaxHunger;
    public int petMaxWellbeing;

    public int maxCultPointsPerPoop;
    public int minCultPointsPerPoop;

    public int wellbeingCheckForPoopAroungMinCooldown;
    public int wellbeingCheckForPoopAroungMaxCooldown;

    public int plantTimeToGrab;
    public float ratReduceCooldownPerPlant;
    public float wellbeingAddedPerHumanFeed;

    [Header("Level 0")]
    public float hungerMaxCooldownLevel0;
    public float hungerMinCooldownLevel0;

    public int hungerLossMinLevel0;
    public int hungerLossMaxLevel0;

    public float poopMaxCooldownLevel0;
    public float poopMinCooldownLevel0;

    public int wellbeingMinLossPerNotPoopLevel0;
    public int wellbeingMaxLossPerNotPoopLevel0;

    public int wellbeingMinLossPerPoopLevel0;
    public int wellbeingMaxLossPerPoopLevel0;

    [Header("Level 1")]

    public int daysForLevel1;
    public float hungerMaxCooldownLevel1;
    public float hungerMinCooldownLevel1;

    public int hungerLossMinLevel1;
    public int hungerLossMaxLevel1;

    public float poopMaxCooldownLevel1;
    public float poopMinCooldownLevel1;

    public int wellbeingMinLossPerNotPoopLevel1;
    public int wellbeingMaxLossPerNotPoopLevel1;

    public int wellbeingMinLossPerPoopLevel1;
    public int wellbeingMaxLossPerPoopLevel1;

    [Header("Level 2")]

    public int daysForLevel2;
    public float hungerMaxCooldownLevel2;
    public float hungerMinCooldownLevel2;

    public int hungerLossMinLevel2;
    public int hungerLossMaxLevel2;

    public float poopMaxCooldownLevel2;
    public float poopMinCooldownLevel2;

    public int wellbeingMinLossPerNotPoopLevel2;
    public int wellbeingMaxLossPerNotPoopLevel2;

    public int wellbeingMinLossPerPoopLevel2;
    public int wellbeingMaxLossPerPoopLevel2;

    [Header("Food")]
    public float foodRatValue;
    public float foodVegetablesValue;
    public float foodHumanValue;

    [Header("Rat")]
    public float RatSpawnCooldown;
    public float RatSpeedLevel0;
    public float RatSpeedLevel1;
    public float RatSpeedLevel2;

    [Header("Human")]
    public float initialHumanCost;
    public float costIncreasePerPurchase;

    [Header("Plant")]
    public float maxWaterForPlant;
    public float waterPerDrop;

    [Header("Mini Game")]
    public int miniGameDurationLvl0;
    public int miniGameDurationLvl1;
    public int miniGameDurationLvl2;

    public int miniGameSuccessNeededLvl0;
    public int miniGameSuccessNeededLvl1;
    public int miniGameSuccessNeededLvl2;

    public float miniGameWellbeingCostForLosing;
    public float miniGameWellbeingForSuccess;
    public float miniGameTimeDecreasePerFailure;
    public float miniGameTimeIncreasePerSuccess;

}

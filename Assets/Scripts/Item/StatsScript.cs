using TMPro;
using UnityEngine;

public class StatsScript : MonoBehaviour
{
    [SerializeField] private TMP_Text statsName;
    [SerializeField] private TMP_Text statsValue;

    public void Setup(string statsName, string statsValue)
    {
        this.statsName.text = statsName;
        this.statsValue.text = statsValue;
    }
}

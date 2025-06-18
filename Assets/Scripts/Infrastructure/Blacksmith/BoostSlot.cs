using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostSlot : MonoBehaviour
{
    [SerializeField] private Image BoostIcon;
    [SerializeField] private TMP_Text BoostItemName;
    [SerializeField] private TMP_Text BoostItemCost;

    private System.Action<Boost> onBoostClickedCallback;
    private Boost boost;
    private Button button;

    private void Awake()
    {
        button = transform.parent.GetComponent<Button>();
    }

    public void Setup(Boost boost, System.Action<Boost> callback)
    {
        this.boost = boost;
        onBoostClickedCallback = callback;

        if(boost != null)
        {
            BoostIcon.sprite = boost.boostSprite;
            BoostItemName.text = boost.boostName;
            BoostItemCost.text = boost.boostCost.ToString();
        }
        else
        {
            BoostIcon.enabled = false;
            BoostItemName.text = "NULL";
            BoostItemCost.text = "NULL";
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            onBoostClickedCallback?.Invoke(GetBoost());
        });
    }

    public Boost GetBoost() => boost;
}

using TMPro;
using UnityEngine;

namespace Code.Gold
{
    public class GoldUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldText;

        public void UpdateGold(int gold)
        {
            goldText.text = gold.ToString();
        }
    }
}
using Code.Managers;
using TMPro;
using UnityEngine;

namespace Code.DoBak
{
    public class DoBakUI : MonoBehaviour
    {
        [SerializeField] private DataManager dataManager;
        [SerializeField] private TMP_InputField goldBetting;
        [SerializeField] private GameObject warringPanel;
        [SerializeField] private GameObject bettingPanel;
        [SerializeField] private Animator animator;

        public void CanBetting()
        {
            if (int.Parse(goldBetting.text) <= dataManager.Gold)
            {
                dataManager.Gold -= int.Parse(goldBetting.text);
                //animator.SetBool();
            }
            else
            {
                warringPanel.SetActive(true);
            }
        }

        public void ExitBetting()
        {
            bettingPanel.SetActive(false);
        }
    }
}
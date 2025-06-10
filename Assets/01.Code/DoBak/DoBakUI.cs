using System.Collections;
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
        [SerializeField] private GameObject button;
        [SerializeField] private Animator animator;

        
        [SerializeField] private GameObject button2;
        [SerializeField] private GameObject button3;
        [SerializeField] private GameObject button4;
        
        private int _randomValue;

        public void CanBetting()
        {
            if (int.Parse(goldBetting.text) <= dataManager.Gold)
            {
                StartCoroutine(DoBak());
                bettingPanel.SetActive(false);
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

        public void CanDoBetting()
        {
            bettingPanel.SetActive(true);
            button.SetActive(false);
        }


        private IEnumerator DoBak()
        {
            animator.SetBool("In",false);
            animator.SetBool("DoBak",true);
            yield return new WaitForSeconds(2.2f);
            animator.SetBool("DoBak",false);
            animator.SetBool("Hide",true);
            _randomValue = Random.Range(0, 3);
            button2.SetActive(true);
            button3.SetActive(true);
            button4.SetActive(true);
        }

        public void OnButtonClicked(int value)
        {
            if (value == _randomValue)
            {
                dataManager.Gold += int.Parse(goldBetting.text) * 2;
            }
            else
            {
                dataManager.Gold -= int.Parse(goldBetting.text);
            }
            
            dataManager.UpdateGold();

            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            
            animator.SetBool("DoBak",false);
            animator.SetBool("In",true);
            animator.SetBool("Hide",false);
            
            button.SetActive(true);
        }
    }
}
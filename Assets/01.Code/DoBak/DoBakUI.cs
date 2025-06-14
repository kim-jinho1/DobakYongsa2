// DoBakUI.cs
using System.Collections;
using Code.CameraSetting;
using Code.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Code.DoBak
{
    public class DoBakUI : MonoBehaviour
    {
        
        [SerializeField] private ParticleSystem successEffect;
        [SerializeField] private ParticleSystem failEffect;
        
        [SerializeField] private DataManager dataManager;
        [SerializeField] private TMP_InputField goldBetting;
        [SerializeField] private GameObject warringPanel;
        [SerializeField] private GameObject bettingPanel;
        [SerializeField] private GameObject button;
        [SerializeField] private GameObject button1;
        [SerializeField] private Animator animator;

        [SerializeField] private GameObject button2;
        [SerializeField] private GameObject button3;
        [SerializeField] private GameObject button4;

        public UnityEvent OnExitDoBak;

        private int _randomValue;

        public void CanBetting()
        {
            if (int.Parse(goldBetting.text) <= dataManager.Gold)
            {
                var player = FindObjectOfType<Code.Players.Player>();
                if (player != null)
                {
                    player.DoBakChange();
                }

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

        public void Close(GameObject ga)
        {
            ga.SetActive(false);
            CameraRotation.IsUI = false;
            OnExitDoBak?.Invoke();

            var player = FindObjectOfType<Code.Players.Player>();
            if (player != null)
                player.IsDoingDoBak = false; // 도박 상태 해제
        }

        public void CanDoBetting()
        {
            bettingPanel.SetActive(true);
            button.SetActive(false);
            button1.SetActive(false);
        }

        private IEnumerator DoBak()
        {
            animator.SetBool("In", false);
            animator.SetBool("DoBak", true);
            yield return new WaitForSeconds(2.2f);
            animator.SetBool("DoBak", false);
            animator.SetBool("Hide", true);
            _randomValue = Random.Range(0, 3);
            button2.SetActive(true);
            button3.SetActive(true);
            button4.SetActive(true);
        }

        public void OnButtonClicked(int value)
        {
            int betAmount = int.Parse(goldBetting.text);

            if (value == _randomValue)
            {
                dataManager.Gold += betAmount * 2;
                successEffect.Play(); // 성공 파티클
            }
            else
            {
                dataManager.Gold -= betAmount;
                failEffect.Play(); // 실패 파티클
            }

            dataManager.UpdateGold();

            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);

            animator.SetBool("DoBak", false);
            animator.SetBool("In", true);
            animator.SetBool("Hide", false);

            button.SetActive(true);
            button1.SetActive(true);
        }

    }
}

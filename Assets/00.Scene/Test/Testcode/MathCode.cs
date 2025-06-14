using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _00.Scene.Test.Testcode
{
    public class MathCode : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentPercentText;
        [SerializeField] private TextMeshProUGUI downPercentText;
        [SerializeField] private TextMeshProUGUI currentEnforceText;
        [SerializeField] private TextMeshProUGUI enforceText;
        
        
        private readonly int _startPercent = 90;
        private readonly int _downPercent = 5;
        private int _currentPercent;
        private int _currentEnforce = 1;

        private void Awake()
        {
            _currentPercent = _startPercent;
        }

        private void Start()    
        {
            TextChange();
        }

        public void ClickButton()
        {
            _currentPercent = _startPercent - _downPercent * _currentEnforce;
            _currentPercent = Mathf.Clamp(_currentPercent, 0, 100);
            
            bool success = (Random.Range(0, 100) < _currentPercent);

            if (success)
            {
                _currentEnforce++;
                enforceText.text = "강화 성공";
            }
            else
            {
                enforceText.text = "강화 실패";
            }

            TextChange();
        }

        private void TextChange()
        {
            currentPercentText.text = $"현재 성공확률 {_currentPercent.ToString()}";
            downPercentText.text = $"감소되는 확률 {_downPercent.ToString()}";
            currentEnforceText.text = $"현재 강화 단계 {_currentEnforce.ToString()}";
        }
    }
}
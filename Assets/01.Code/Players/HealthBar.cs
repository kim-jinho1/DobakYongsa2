using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthText;  // 추가

    private Tween currentTween;

    public void SetHealth(float current, float max)
    {
        float target = current / max; 
        
        currentTween?.Kill();
        currentTween = fillImage.DOFillAmount(target, 0.3f).SetEase(Ease.OutCubic);
        
        healthText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
    }
}
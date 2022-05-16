using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : View<HealthBarView, HealthBar>
{
    [SerializeField] private Image bar;

    public void SetAmount(float amount, float delay)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateAmount(amount, delay));
    }

    private IEnumerator AnimateAmount(float amount, float delay)
    {
        float start = bar.fillAmount;
        for (float t = 0f; t < delay; t += Time.deltaTime)
        {
            bar.fillAmount = Mathf.Lerp(start, amount, t / delay);
            yield return null;
        }
        bar.fillAmount = amount;
    }
}

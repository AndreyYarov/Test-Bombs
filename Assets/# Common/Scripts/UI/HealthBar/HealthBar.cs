using UnityEngine;

public class HealthBar : Presenter<HealthBarView, HealthBar>
{
    private static readonly Vector3 offset = Vector3.up;
    private static HealthBarView prefab;

    private Transform followTarget;
    private RectTransform owned;

    public void SetHealth(float health, float maxHealth, bool animated)
    {
        View.SetAmount(health / maxHealth, animated ? 0.5f : 0f);
    }

    private void Start()
    {
        owned = View.transform as RectTransform;
    }

    private void Update()
    {
        if (followTarget && owned)
        {
            Vector3 pos = followTarget.position + offset;
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(pos);
            Vector2 screenPosition = new Vector2((viewportPosition.x - 0.5f) * UIParent.rect.width, (viewportPosition.y - 0.5f) * UIParent.rect.height);

            owned.anchoredPosition = screenPosition;
            owned.localScale = Vector3.one;
        }
    }

    public static HealthBar Create(Transform followTarget)
    {
        if (!prefab)
            prefab = Resources.Load<HealthBarView>("Health-Bar");

        HealthBarView view = Object.Instantiate(prefab, UIParent);
        view.Presenter.followTarget = followTarget;
        return view.Presenter;
    }
}

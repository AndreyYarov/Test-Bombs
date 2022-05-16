using UnityEngine;
using UniRx;

public class NpcHealth : PlayModeBehaviour
{
    private Collider npcCollider;
    private float health, maxHealth;
    public float Health => health;
    private HealthBar healthBar;

    private void Awake()
    {
        npcCollider = GetComponent<Collider>();
    }

    public void Init(int health)
    {
        maxHealth = this.health = health;
        healthBar = HealthBar.Create(transform);
        healthBar.SetHealth(maxHealth, maxHealth, false);
    }

    private void GetDamage(DamageMessage msg)
    {
        float sqrDistance = Vector3.SqrMagnitude(npcCollider.ClosestPoint(msg.center) - msg.center);
        float sqrRadius = msg.radius * msg.radius;
        if (sqrDistance < sqrRadius)
        {
            float damage = msg.damage * (1f - sqrDistance / sqrRadius);
            if (damage > health)
                damage = health;
            health -= damage;
            healthBar.SetHealth(health, maxHealth, true);
        }
    }

    public CompositeDisposable disposables;
    void OnEnable()
    {
        disposables = new CompositeDisposable();
        MessageBroker.Default.Receive<DamageMessage>().Subscribe(msg => GetDamage(msg)).AddTo(disposables);
    }
    void OnDisable()
    { 
        if (disposables != null)
            disposables.Dispose();
    }

    private void OnDestroy()
    {
        if (healthBar != null)
            healthBar.Destroy();
    }
}

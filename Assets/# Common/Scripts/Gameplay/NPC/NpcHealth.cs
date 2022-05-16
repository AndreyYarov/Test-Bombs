using UnityEngine;
using UniRx;

public class NpcHealth : PlayModeBehaviour
{
    private Collider npcCollider;
    private float health;
    public float Health => health;

    private void Awake()
    {
        npcCollider = GetComponent<Collider>();
    }

    public void Init(int health)
    {
        this.health = health;
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
            Debug.Log($"{name} get damage {damage}");
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
}

using System.Collections;
using UnityEngine;
using UniRx;

public class BombPipeline : EntityPipeline<BombPipeline, BombTemplate>
{
    public override IEnumerator Run()
    {
        Vector3 position = new Vector3(
            Random.Range(-GameController.BombAreaSize, GameController.BombAreaSize),
            GameController.BombSpawnY,
            Random.Range(-GameController.BombAreaSize, GameController.BombAreaSize));
        var bomb = Object.Instantiate(Template.Prefab, position, Quaternion.identity);

        var bombDamage = bomb.AddComponent<BombDamage>();
        Collision collision = null;
        bombDamage.Init(c => collision = c);

        while (collision == null)
            yield return null;

        MessageBroker.Default.Publish(new DamageMessage(Template.Damage, Template.Radius, bomb.transform.position));
        Object.Destroy(bomb);
    }
}

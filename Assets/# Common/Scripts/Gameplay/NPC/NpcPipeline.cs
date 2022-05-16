using System.Collections;
using UnityEngine;

public class NpcPipeline
{
    private NpcController prefab;
    private int health;
    private float walkAreaSize;
    private float speed;

    public NpcPipeline(NpcController prefab, int health, float walkAreaSize, float speed)
    {
        this.prefab = prefab;
        this.walkAreaSize = walkAreaSize;
        this.health = health;
        this.speed = speed;
    }

    public IEnumerator Run()
    {
        var npc = GameObject.Instantiate(prefab);
        npc.Walk(walkAreaSize, speed);

        var npcHealth = npc.gameObject.AddComponent<NpcHealth>();
        npcHealth.Init(health);

        while (npcHealth.Health > 0)
            yield return null;

        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(npc.gameObject);
    }
}

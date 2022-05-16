using System.Collections;
using UnityEngine;

public class NpcPipeline : EntityPipeline<NpcPipeline, NpcTemplate>
{
    public NpcPipeline() { }

    public override IEnumerator Run()
    {
        var npc = Object.Instantiate(Template.Prefab);
        npc.Walk(GameController.WalkAreaSize, Template.Speed);

        var npcHealth = npc.gameObject.AddComponent<NpcHealth>();
        npcHealth.Init(Template.Health);

        while (npcHealth.Health > 0)
            yield return null;

        yield return new WaitForSeconds(0.5f);
        Debug.Log($"{npc.name} damaged");
        Object.Destroy(npc.gameObject);
    }
}

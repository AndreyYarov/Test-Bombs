using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float walkAreaSize;

    private void Start()
    {
        GameConfig config = Resources.Load<GameConfig>("Game-Config");
        if (!config)
        {
            DestroyImmediate(gameObject);
            return;
        }
        for (int i = 0; i < config.npcTemplateCount; i++)
            StartCoroutine(SpawnNPCs(config.GetNpcTemplate(i)));
    }

    private IEnumerator SpawnNPCs(NpcTemplate template)
    {
        for (int i = 0; i < template.Count || template.Count == 0; i++)
        {
            float delay = i == 0 ? template.Time : template.SpawnDelay;
            while (delay > 0)
            {
                yield return null;
                delay -= Time.deltaTime;
            }
            NpcPipeline pipeline = new NpcPipeline(template.Prefab, template.Health, walkAreaSize, template.Speed);
            StartCoroutine(pipeline.Run());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0.5f, 0.75f);
        Gizmos.DrawCube(Vector3.up * 0.1f, new Vector3(walkAreaSize * 2, 0f, walkAreaSize * 2));

        Gizmos.color = new Color(1f, 1f, 0.5f, 1f);
        Gizmos.DrawWireCube(Vector3.up * 0.1f, new Vector3(walkAreaSize * 2, 0f, walkAreaSize * 2));
    }
}

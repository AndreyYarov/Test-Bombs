using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float walkAreaSize;
    [SerializeField] private float bombAreaSize;
    [SerializeField] private float bombSpawnY;

    private static GameController game;
    public static GameController Game => game;

    public static float WalkAreaSize => game.walkAreaSize;
    public static float BombAreaSize => game.bombAreaSize;
    public static float BombSpawnY => game.bombSpawnY;

    private void Awake()
    {
        game = this;
    }

    private void Start()
    {
        GameConfig config = Resources.Load<GameConfig>("Game-Config");
        if (!config)
        {
            DestroyImmediate(gameObject);
            return;
        }
        for (int i = 0; i < config.npcTemplateCount; i++)
            StartCoroutine(Spawn<NpcPipeline, NpcTemplate>(config.GetNpcTemplate(i)));
        for (int i = 0; i < config.bombTemplateCount; i++)
            StartCoroutine(Spawn<BombPipeline, BombTemplate>(config.GetBombTemplate(i)));
    }

    private IEnumerator Spawn<P, T>(T template)
        where P : EntityPipeline<P, T>, new()
        where T : Template
    {
        if (template.ReadyForSpawn())
        {
            float delay = template.Time;
            for (int i = 0; i < template.Count || template.Count == 0; i++, delay += template.SpawnDelay)
            {
                while (delay > 0)
                {
                    yield return null;
                    delay -= Time.deltaTime;
                }
                P pipeline = EntityPipeline<P, T>.Create(template);
                StartCoroutine(pipeline.Run());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0.5f, 0.75f);
        Gizmos.DrawCube(Vector3.up * 0.05f, new Vector3(walkAreaSize * 2, 0f, walkAreaSize * 2));

        Gizmos.color = new Color(1f, 1f, 0.5f, 1f);
        Gizmos.DrawWireCube(Vector3.up * 0.05f, new Vector3(walkAreaSize * 2, 0f, walkAreaSize * 2));


        Gizmos.color = new Color(1f, 0f, 0f, 0.75f);
        Gizmos.DrawCube(Vector3.up * bombSpawnY, new Vector3(bombAreaSize * 2, 0f, bombAreaSize * 2));

        Gizmos.color = new Color(0.8f, 0f, 0f, 1f);
        Gizmos.DrawWireCube(Vector3.up * bombSpawnY, new Vector3(bombAreaSize * 2, 0f, bombAreaSize * 2));
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Test-Bombs/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private List<NpcTemplate> npcTemplates = new List<NpcTemplate>();
    public int npcTemplateCount => npcTemplates.Count;
    public NpcTemplate GetNpcTemplate(int index) => npcTemplates[index];


    [SerializeField] private List<BombTemplate> bombTemplates = new List<BombTemplate>();
    public int bombTemplateCount => bombTemplates.Count;
    public BombTemplate GetBombTemplate(int index) => bombTemplates[index];
}

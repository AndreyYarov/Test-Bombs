using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Test-Bombs/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private List<NpcTemplate> npcTemplates = new List<NpcTemplate>();
    public int npcTemplateCount => npcTemplates.Count;
    public NpcTemplate GetNpcTemplate(int index) => npcTemplates[index];
}

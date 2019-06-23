using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Scrap this class if no `classes` are implemented.
[CreateAssetMenu(fileName = "class", menuName = "Game/Class", order = 1)]
public class CharacterClass : ScriptableObject
{
    // Movement stats   ==================
    [Range(0.1f, 3f)]
    public float moveSpeed = 1.8f;
    [Range(0.1f, 4f)]
    public float runSpeed = 2.5f;
    [Range(0.1f, 2f)]
    public float carrySpeed = 1f;


    // Combat stats     ==================

    public int initialHealth = 10;
    public int maxHealth = 30;

}

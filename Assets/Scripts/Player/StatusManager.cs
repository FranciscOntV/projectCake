using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public CharacterClass character;
    public int currentHP;
    public int maxHP;
    public float SPD;
    public float STR;
    public float JMP;
    // TODO: move to Common if going to be standard in al characters.
    public float runMultiplier = 1.5f;


    void Awake()
    {
        initialize();
    }

    /// <summary>
    /// Initializes the values to the default amount.
    /// </summary>
    public void initialize()
    {
        currentHP = maxHP;
    }

    /// <summary>
    /// Changes the current HP value.
    /// </summary>
    /// <param name="amount">The amount to add or substract.</param>
    public void changeHP(int amount)
    {
        this.currentHP += amount;
        this.currentHP = (this.currentHP < 0 ? 0 : (this.currentHP > this.maxHP ? this.maxHP : this.currentHP));
    }

    /// <summary>
    /// Determines if character is dead.
    /// </summary>
    /// <returns>
    /// bool true if current HP = 0, otherwise false.
    /// </returns>
    public bool isDead()
    {
        return (currentHP <= 0);
    }
}

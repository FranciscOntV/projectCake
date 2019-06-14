using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public CharacterClass character;
    private int currentHP;
    private int maxHP;
    private float walkSpeed;
    private float runSpeed;
    private float slowSpeed;


    public void initialize()
    {
        this.currentHP = this.maxHP = this.character.initialHealth;
        this.walkSpeed = this.character.moveSpeed;
        this.runSpeed = this.character.runSpeed;
        this.slowSpeed = this.character.carrySpeed;
    }

    public float getWalkSpeed() {
        return this.walkSpeed;
    }

    public int[] getHp()
    {
        return new int[2] { this.currentHP, this.maxHP };
    }

    public void changeHP(int amount)
    {
        this.currentHP += amount;
        this.currentHP = (this.currentHP < 0 ? 0 : (this.currentHP > this.maxHP ? this.maxHP : this.currentHP));

    }

    // Update is called once per frame
    void Update()
    {

    }
}

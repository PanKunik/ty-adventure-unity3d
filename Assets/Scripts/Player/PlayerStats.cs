﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    Image healthBar;
    Image expBar;
    Text levelTxt;

    Animator anim;
    PlayerMovement playerMov;
    PlayerController playerControl;
    CharacterCombat Enemy;

    private int level;
    private int nextLevelExperience;
    public int Experience { get; set; }

    void Update()
    {
        if( Experience >= nextLevelExperience )
            LevelUp();

        expBar.fillAmount = (Experience / (float)nextLevelExperience);
    }

    void LevelUp()
    {
        level++;
        nextLevelExperience = (int)((level / 2) + Mathf.Log10(2 * level + 1) * 1000 * level);
        levelTxt.text = level.ToString();
        Debug.Log("LEVEL UP! " + level);
    }
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        Experience = 0;
        level = 1;
        nextLevelExperience = (int)((level/2) + Mathf.Log10(2*level+1) * 1000*level);

        healthBar = GameObject.Find("FillHealth").GetComponent<Image>();
        expBar = GameObject.Find("FillXP").GetComponent<Image>();
        levelTxt = GameObject.Find("Level").GetComponent<Text>();
        levelTxt.text = level.ToString();

        playerMov = GetComponent<PlayerMovement>();
        playerControl = GetComponent<PlayerController>();
        Enemy = GameObject.FindWithTag("Enemy").GetComponent<CharacterCombat>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.fillAmount = (currentHealth / (float)maxHealth);
    }

    public override void Die()
    {
        base.Die();
        anim.SetTrigger("Die");
        playerMov.enabled = false;
        playerControl.enabled = false;
        Enemy.enabled = false;
    }
}

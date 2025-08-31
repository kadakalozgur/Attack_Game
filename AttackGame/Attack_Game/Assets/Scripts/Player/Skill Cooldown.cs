using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    public PlayerCombat playerCombat; 
    public Image cooldownImage;       

    void Update()
    {

        float cooldown = playerCombat.attack2Cooldown;
        float timeSinceLastUse = Time.time - playerCombat.lastAttack2Time;
        float fillAmount = Mathf.Clamp01(timeSinceLastUse / cooldown);

        cooldownImage.fillAmount = fillAmount;

    }
}

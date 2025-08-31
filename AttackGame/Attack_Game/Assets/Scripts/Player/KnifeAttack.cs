using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public BoxCollider boxCollider;

    private float bloodEffectDuration = 0.3f;
    private HashSet<GameObject> hitTargets = new HashSet<GameObject>();
    private bool canDamage = false;
    private bool hasHitTarget = false;
    private float damage;

    public void enableBoxColl()
    {

        boxCollider.enabled = true;

        canDamage = true;

        hitTargets.Clear();

        hasHitTarget = false;

    }

    public void disableBoxColl()
    {

        boxCollider.enabled = false;

        canDamage = false;

        hitTargets.Clear();

        hasHitTarget = false;

    }

    public bool HasHitAnyTarget()
    {

        return hasHitTarget;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage || hitTargets.Contains(other.gameObject))
            return;

        if (other.CompareTag("enemyboss"))
        {

            if (playerCombat.attack2)
            {

                damage = 15f;

            }

            else
            {

                damage = 8f;

            }

        }


        else if (other.CompareTag("enemysmallboss"))
        {

            if (playerCombat.attack2)
            {

                damage = 30f;

            }

            else
            {

                damage = 15f;

            }

        }


        else if (other.CompareTag("enemysoldier"))
        {

            if (playerCombat.attack2)
            {

                damage = 50f;

            }

            else
            {

                damage = 25f;

            }

        }

        else
            return;


        if (!hasHitTarget)
        {
            hasHitTarget = true;

            if (playerCombat.attack2)
                playerCombat.playDoubleHitSound();

            else
                playerCombat.playHitSound();
        }

        hitTargets.Add(other.gameObject);

        EnemyHealthBar enemyHealth = other.GetComponent<EnemyHealthBar>();

        if (enemyHealth != null)
            enemyHealth.takeDamage(damage);

        Transform effectTransform = other.transform.Find("BloodEffect");

        if (effectTransform != null)
        {

            GameObject effect = effectTransform.gameObject;

            effect.SetActive(true);

            StartCoroutine(DeactivateAfterTime(effect, bloodEffectDuration));

        }
    }

    private IEnumerator DeactivateAfterTime(GameObject effect, float duration)
    {

        yield return new WaitForSeconds(duration);

        effect.SetActive(false);

    }
}

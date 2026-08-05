using System.Collections;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamegePerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }
    private IEnumerator DealDamegePerformer(DealDamageGA dealDamageGA)
    {
        foreach(var target in dealDamageGA.Targets)
        {
            target.Damage(dealDamageGA.Amount);
            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
            if(target.CurrentHealth <= 0
                && target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }
        }
    }
}

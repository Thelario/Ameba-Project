using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Defense : MonoBehaviour
{
    public Defenses defenseType;

    public float moveSpeed;

    private Rigidbody2D rb;
    private Settings set;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        set = Settings.GetInstance();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, 0f);
    }

    /// <summary>
    /// When a collision between an enemy and a defense is produced, we check for a Manager in order to figure out the result of the battle.
    /// Depending on the result of the battle, defense will be destroyed, enemy will be destroyed, or both.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            BattleState bs = TypesManager.GetInstance().DefenseDestroysEnemy(collision.collider.GetComponent<Enemy>().enemyType, defenseType);

            switch (bs)
            {
                case BattleState.DefenseWins:
                    Destroy(collision.collider.gameObject);
                    SoundManager.PlaySound(SoundManager.Sound.DefenseKillEnemy, transform.position, set.sfxVolume);
                    break;

                case BattleState.EnemyWins:
                    Destroy(gameObject);
                    SoundManager.PlaySound(SoundManager.Sound.EnemyKillDefense, transform.position, set.sfxVolume);
                    break;

                case BattleState.Neutral:
                    Destroy(gameObject);
                    Destroy(collision.collider.gameObject);
                    SoundManager.PlaySound(SoundManager.Sound.DefenseKillEnemy, transform.position, set.sfxVolume);
                    break;
            }

            Destroy(Instantiate(Assets.i.destroyDefenseEnemyParticles, transform.position, Quaternion.identity), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Infection"))
        {
            StatsBattleManager.GetInstance().DamageInfection();
            SoundManager.PlaySound(SoundManager.Sound.HitHeartInfection, transform.position, set.sfxVolume);
            Destroy(Instantiate(Assets.i.infectionHitParticles, transform.position + new Vector3(0.5f, 0f), Quaternion.identity), 1f);
            Destroy(gameObject);
        }
    }
}

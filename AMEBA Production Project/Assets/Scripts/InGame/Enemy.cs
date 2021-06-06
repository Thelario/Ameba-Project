using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Enemy : MonoBehaviour
{
    public Enemies enemyType;

    public float moveSpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StatsBattleManager.GetInstance().IncreaseTemperature();
            SoundManager.PlaySound(SoundManager.Sound.HitHeartInfection, transform.position, Settings.GetInstance().sfxVolume);
            Destroy(Instantiate(Assets.i.heartHitParticles, transform.position + new Vector3(-0.5f, 0f), Quaternion.Euler(new Vector3(0f, 180f, 0f))), 1f);
            Destroy(gameObject);
        }
    }
}

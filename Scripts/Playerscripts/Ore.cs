using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Ore: MonoBehaviour
{
    [SerializeField] private float oreHealth = 15f;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject breakParticle;
    [SerializeField] private float hitEffectCooldown = 0.2f;
    private float currentHealth;
    private bool isBroken = false;
    private Coroutine pulseCoroutine;
    private Vector3 originalScale;
    private float lastHitEffectTime;

    // Назначение всякого
    void Start()
    {
        currentHealth = oreHealth;
        originalScale = transform.localScale;
        lastHitEffectTime = -hitEffectCooldown;
    }

    // Руда сосет урон
    public void ApplyDamage(float amount)
    {
        if (isBroken) return;
        currentHealth -= amount;
        Debug.Log($"Руда: {currentHealth}/{oreHealth}");

        if (Time.time >= lastHitEffectTime + hitEffectCooldown)
        {
            PlayHitEffect();
            lastHitEffectTime = Time.time;
        }
        
        if (currentHealth <= 0)
        {
            BreakOre();
        }
    }

    private void PlayHitEffect()
    {
        if (pulseCoroutine != null) StopCoroutine(pulseCoroutine);
        pulseCoroutine = StartCoroutine(PulseRoutine());
    }

    private IEnumerator PulseRoutine()
    {
        float elapsed = 0f;
        float duration = 0.1f;
        float scaleFactor = 1.2f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float scale = 1f + (scaleFactor - 1f) * (1f - Mathf.Abs(t * 2f - 1f));
            transform.localScale = originalScale * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        pulseCoroutine = null;
    }

    // Ломание руды
    private void BreakOre()
    {
        isBroken = true;

        if (breakParticle != null)
            Instantiate(breakParticle, transform.position, Quaternion.identity);

        // Дроп руды короче
        if (drop != null)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            Instantiate(drop, transform.position + randomOffset, Quaternion.identity);
            Debug.Log("СПАВН РУДЫ!");
        }
        
        Destroy(gameObject);
    }
}

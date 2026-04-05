using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            float duration = ps.main.duration;
            if (ps.main.loop) duration = 0.1f;
            Destroy(gameObject, duration + 1f);
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}

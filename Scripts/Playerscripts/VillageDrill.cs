using UnityEngine;
using TMPro;

// Обрезка скрипта дрели для входа в дрели деревни, тупо воспринятие кнопки E
public class VillageDrill : MonoBehaviour
{
    public DrillController drill;
    public float enterDistance = 2f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, drill.transform.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryEnter();
        }
    }

    void TryEnter()
    {
        float distance = Vector2.Distance(transform.position, drill.transform.position);
        if (distance > enterDistance) return;

        SceneTransition.SwitchToScene("Play");
    }

    void OnDrawGizmosSelected()
    {
        if (drill == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(drill.transform.position, enterDistance);
    }
}
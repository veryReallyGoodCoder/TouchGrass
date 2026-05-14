using UnityEngine;

public class SunBehaviourScript : MonoBehaviour
{
    private ParticleSystem ps;

    

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();

        if(ps != null)
        {
            Debug.Log("particle system found");
        }
        else
        {
            Debug.Log("ERROR particle system not found");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var psEmissions = ps.emission;

        if (collision.CompareTag("Enemy"))
        {
            psEmissions.enabled = false;
            Debug.Log("enemy blocking sun");
            return;
        }

        /*if (collision.CompareTag("Player"))
        {
            psEmissions.enabled = true;
            Debug.Log($"player shining {ps.emission.enabled}");
        }*/
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        var psEms = ps.emission;

        if (collision.CompareTag("Enemy"))
        {
            psEms.enabled = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTrash : MonoBehaviour
{
    [SerializeField] GameObject effects = null;
    private GameObject effectsTrack = null;

    void Update()
    {
        if (effectsTrack != null)
        {
            if (effectsTrack.GetComponent<ParticleSystem>().isPlaying == false)
            {
                Destroy(effectsTrack);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            effectsTrack = Instantiate(effects, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}

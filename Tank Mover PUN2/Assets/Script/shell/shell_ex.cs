using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_ex : MonoBehaviour
{
    public ParticleSystem myParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myParticleSystem != null && myParticleSystem.particleCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}

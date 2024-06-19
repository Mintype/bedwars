using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gernade : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public GameObject explosionEffect;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private bool hasBlownUp = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!hasBlownUp)
            Invoke("blowUp", 1.5f);
    }

    public void blowUp()
    {
        if (!hasBlownUp)
        {
            print("boom");
            GameObject exp = Instantiate(explosionEffect, transform.position, transform.rotation);

            //audioSource.PlayOneShot(audioClip);
            //AudioSource a = Instantiate(audioSource, transform.position, transform.rotation);

/*            if (audioSource == null)
                print("audiosource is null");

            audioSource.clip = audioClip;
            //audioSource.clip = audioClip;
            audioSource.Play();*/

            AudioSource audioSource1 = exp.AddComponent<AudioSource>(); // Adding AudioSource component

            if (audioSource1 == null)
                print("audiosource is null");

            audioSource1.clip = audioClip;
            audioSource1.Play();

            Destroy(exp, 2.0f);
            /*Destroy(a, 2.0f);*/
            hasBlownUp = true;
            Destroy(gameObject); // destroy the gernade after blowing up.
        } else
        {
            Destroy(gameObject); // destroy the gernade after blowing up.
        }
    }
}

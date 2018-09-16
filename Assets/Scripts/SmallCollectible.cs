using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCollectible : MonoBehaviour {

    [SerializeField] AudioClip collectSFX;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameSession.Instance.AddSmallCollectible();
        PlaySound(collectSFX);
        Destroy(gameObject);
    }

    void PlaySound(AudioClip sfx)
    {
        AudioSource.PlayClipAtPoint(collectSFX, Camera.main.transform.position);
    }
}

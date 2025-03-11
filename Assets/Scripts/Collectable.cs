using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Spawner spawner;
    public AudioClip collectSound;
    private AudioSource audioSource;
    private bool canCollect = true;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (canCollect)
        {
            audioSource?.PlayOneShot(collectSound);
            spawner?.DropCollected(gridPosition);
            GameManager.AddScore();
            canCollect = false;
        }
    }
}
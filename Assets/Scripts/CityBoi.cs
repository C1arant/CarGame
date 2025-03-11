using UnityEngine;

public class CityBoi : MonoBehaviour
{
    public GameObject RagDoll;
    public GameObject Character;
    public string TagTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        RagDoll.SetActive(true);
        Character.SetActive(false);
    }
}

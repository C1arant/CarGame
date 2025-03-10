using UnityEngine;

public class CityBoi : MonoBehaviour
{
    public GameObject RagDoll;
    public GameObject Character;
    public string TagTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagTrigger));
        {
            RagDoll.SetActive(true);
            Character.SetActive(false);
        }
    }
}

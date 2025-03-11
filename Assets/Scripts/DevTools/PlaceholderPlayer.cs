using Unity.Netcode;
using UnityEngine;

public class PlaceholderPlayer : NetworkBehaviour
{
    [SerializeField] private Camera _playerCamera;

    private void Start()
    {
        if(IsOwner)
        {
            this._playerCamera.gameObject.SetActive(true);
        } else
        {
            Destroy(this._playerCamera.gameObject);
        }
    }
}
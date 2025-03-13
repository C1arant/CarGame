using UnityEngine;
using UnityEngine.EventSystems;

public class SetEventSelected : MonoBehaviour
{
    EventSystem m_EventSystem;
    public GameObject Button;

    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;
        m_EventSystem.SetSelectedGameObject(Button);
    }
}

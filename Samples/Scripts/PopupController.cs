using UnityEngine;

public class PopupController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // show popup on start
        PopupBuilder.Create("Delete !")
            .SetMessage("Are you sure you want to delete this item?")
            .AddButton("Cancel" , () => Debug.Log("Cancel"))
            .AddButton("Delete" , () => Debug.Log("Delete"))
            .Show();
    }
}

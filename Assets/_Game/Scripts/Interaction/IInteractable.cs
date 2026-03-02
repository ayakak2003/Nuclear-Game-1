using UnityEngine;


public interface IInteractable
{
    void Interact();
    string GetDescription(); // Useful for UI: "Press E to Open", "Press E to Hack"
}
using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;
    bool hasInterected = false;
    public virtual void Interact()
    {
        // To be Overwritten by object or npc
    }
    void Update()
    {
        if (isFocus && !hasInterected)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInterected = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInterected = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInterected = false;
    }
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position,radius);
    }
}

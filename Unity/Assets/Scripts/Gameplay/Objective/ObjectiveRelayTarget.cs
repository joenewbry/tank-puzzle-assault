using UnityEngine;

public class ObjectiveRelayTarget : MonoBehaviour
{
    public ObjectiveTracker targetObjective;
    public string triggerEvent = "OnComplete";
    public GameObject nextScene;

    private void Start()
    {
        if (targetObjective != null)
        {
            targetObjective.OnComplete += RelayToNext;
        }
    }

    private void RelayToNext()
    {
        Debug.Log($"Relaying to next scene: {nextScene.name}");
        // TODO: Load next scene or trigger end state
    }
}

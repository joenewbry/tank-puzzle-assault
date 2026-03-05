using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    public string objectiveName = "Default Objective";
    public bool isCompleted = false;
    public GameObject completionTrigger;

    private void Start()
    {
        if (completionTrigger != null)
        {
            completionTrigger.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCompleted)
        {
            CompleteObjective();
        }
    }

    public void CompleteObjective()
    {
        isCompleted = true;
        Debug.Log($"Objective '{objectiveName}' completed!");
        // TODO: Trigger win state or next level
    }
}

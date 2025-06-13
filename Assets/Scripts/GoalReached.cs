using UnityEngine;

public class GoalReached : MonoBehaviour
{
    public BoardControl BoardControl;

    private void Start()
    {
        this.GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "Sphere")
        {
            BoardControl.GoalReached();
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Renderer>().enabled = false;
        }
    }

    public void EnableColls()
    {
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<Renderer>().enabled = true;
    }
}

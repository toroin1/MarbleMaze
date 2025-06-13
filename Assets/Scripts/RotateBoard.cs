using UnityEngine;

public class RotateBoard : MonoBehaviour
{
    private int maxangle = 10;
    private float speed = 100f;

    public void RotateBoardOnActionRecieved(float actionx, float actionz)
    {
        float rotationx=0;
        float rotationz=0;
        

        rotationx = speed * actionx;
        rotationz = speed * actionz;

        Vector3 updateVector = new Vector3(rotationx,0, rotationz)*Time.fixedDeltaTime;


        if (updateVector.x + transform.eulerAngles.x > maxangle && updateVector.x + transform.eulerAngles.x < 360 - maxangle)
        {
            updateVector.x = 0;

        } 
        if (updateVector.z + transform.eulerAngles.z > maxangle && updateVector.z + transform.eulerAngles.z < 360 - maxangle)
        {
            updateVector.z = 0;

        }

        transform.eulerAngles += updateVector;


    }
}

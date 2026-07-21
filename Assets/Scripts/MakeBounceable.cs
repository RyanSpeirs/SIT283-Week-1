using UnityEngine;

public class MakeBounceable : MonoBehaviour
{
    //  This is our conveypr belt
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.08f;


        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x,y,z) * 4, ForceMode.Impulse);
    }

}

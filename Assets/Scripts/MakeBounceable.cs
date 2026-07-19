using UnityEngine;

public class MakeBounceable : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.08f;


        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x,y,z) * 4, ForceMode.Impulse);
    }

}

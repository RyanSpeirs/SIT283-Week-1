using UnityEngine;

public class MakeBounceable : MonoBehaviour
{
    public GameObject bounceablePrefab;

    public int numberOfBounceables = 1;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateBounceables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void CreateBounceables()
    {
        for (int i = 0; i < numberOfBounceables; i++)
        {
            GameObject g = Instantiate(bounceablePrefab);
            await Awaitable.WaitForSecondsAsync(1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);

        float x = Random.Range (0.2f, 0.8f);
        float y = Random.Range(0.2f, 0.8f);
        float z = Random.Range(0.2f, 0.8f);


        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x,y,z) * 10, ForceMode.Impulse);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory : MonoBehaviour
{
    public List<GameObject> customerPrefabs;
    public Transform customerParent;

    public GameObject CreateCustomer()
    {
        GameObject customer = Instantiate(customerPrefabs[LevelManager.Instance.CurrentLevel], customerParent);
        //customer.transform.Find("Body").GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        return customer;
    }

    public void DeleteCustomer(GameObject customer)
    {
        Destroy(customer);
    }
}

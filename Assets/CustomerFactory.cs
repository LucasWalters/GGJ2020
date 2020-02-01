using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform customerParent;

    public GameObject CreateCustomer()
    {
        GameObject cube = Instantiate(customerPrefab, customerParent);
        cube.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        return cube;
    }

    public void DeleteCustomer(GameObject customer)
    {
        Destroy(customer);
    }
}

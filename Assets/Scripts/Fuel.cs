using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    Collider col;
    Renderer rend;
    Color myColor;
    Color gray = new Color(0.5f, 0.5f, 0.5f, 1f);


    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        col.enabled = true;
        myColor = rend.material.color;
    }


    void Update()
    {
        transform.Rotate(new Vector3(0.2f, -0.2f, 0.2f));
    }

    public IEnumerable UseFuel()
    {
        col.enabled = false;
        rend.material.color = gray;
        rend.material.SetColor("_EmissionColor", gray);
        yield return new WaitForSeconds(10);
        col.enabled = true;
        rend.material.color = myColor;
        rend.material.SetColor("_EmissionColor", myColor);        
    }

    private void OnTriggerEnter(Collider other)
    {
        AddFuel car = other.GetComponent<AddFuel>();
        if(car != null && car.Add())
        {
            StartCoroutine("UseFuel");//this line right here officer
        }
    }
}

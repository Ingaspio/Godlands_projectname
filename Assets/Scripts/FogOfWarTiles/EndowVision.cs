using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndowVision : MonoBehaviour
{
    [SerializeField]
    private float spreadAmount, spreadInterwall = 1f;
    [SerializeField]
    private int radius;
    [SerializeField]
    FoWManager foWManager;

    private void Awake()
    {
        foWManager = FindObjectOfType<FoWManager>();
    }
    //private void Start()
    //{
    //    StartCoroutine(SpreadRoutine());
    //}
    private IEnumerator SpreadRoutine() 
    {
        while (true)
        {
            foWManager.AddVision(transform.position, spreadAmount, radius);

            yield return new WaitForSeconds(spreadInterwall);
        }
    }
}

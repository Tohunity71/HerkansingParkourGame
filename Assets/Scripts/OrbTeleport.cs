using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTeleport : MonoBehaviour
{
    public float orbThrowDistance;
    //public Rigidbody orbTeleport;
    [SerializeField] private Transform Player;
    //[SerializeField] private Transform orbPos;
    GameObject prefab;
    private GameObject orbProjectile;
    private bool orbSpawn = false;
    private bool orbCooldownBool = false;



    void Start()
    {
        //orbTeleport = GetComponent<Rigidbody>();
        prefab = Resources.Load("orbTeleport") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && orbSpawn == false && orbCooldownBool == false)
        {
            orbSpawn = true;
            orbProjectile = Instantiate(prefab) as GameObject;
            orbProjectile.transform.position = transform.position + Camera.main.transform.forward * 2;
            Rigidbody orbTeleportRB = orbProjectile.GetComponent<Rigidbody>();
            orbTeleportRB.velocity = Camera.main.transform.forward * 25;
            StartCoroutine(orbLifetime());
        }
        else if (Input.GetMouseButtonDown(1) && orbSpawn == true)
        {
            orbSpawn = false;
            orbCooldownBool = true; // omdat hij nu true is kan ik niet meer schieten.
            Player.transform.position = orbProjectile.transform.position;
            Destroy(orbProjectile);
            StartCoroutine(orbCooldown());
        }
        

    }

    private IEnumerator orbLifetime()
    {
        yield return new WaitForSeconds(4.5f);
        if (orbSpawn == true)
            orbSpawn = false;
            Destroy(orbProjectile);
    }

    private IEnumerator orbCooldown()
    {
        yield return new WaitForSeconds(5f);
        orbCooldownBool = false;
    }

}



// Je moet ervoor zorgen dat je een leeg gameobject het staan zodat mijn ifstatement 
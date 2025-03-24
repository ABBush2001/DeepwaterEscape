using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Script : MonoBehaviour
{

    public int waveDmg = 4;

    public float moveSpeed = 10f;
    public float moveDistanceX = 5f;
    public float moveDistanceZ = 5f;
    public float scale = 2f;

    // The origial position and the scale
    private Vector3 orignalPos;
    private Vector3 orignalSca;

    // Start is called before the first frame update
    void Start()
    {
        // to start in the position and scale they have 
        orignalPos = transform.position;
        orignalSca = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    private IEnumerator MoveAndScale()
    {
        //
        Vector3 Objposition = new Vector3(transform.position.x + moveDistanceX, transform.position.y, transform.position.z + moveDistanceZ);

        //
        Vector3 ObjScale = new Vector3(transform.localScale.x , transform.localScale.y * scale, transform.localScale.z);

        //
        float startTime = Time.time;
        float length = Vector3.Distance(transform.position, Objposition);

        while (Vector3.Distance(transform.position, Objposition) > 0.1f)
        {
            float farDistance = (Time.time - startTime) * moveSpeed;
            float fraction = farDistance / length;

            transform.position = Vector3.Lerp(transform.position, Objposition, fraction);

            transform.localScale = Vector3.Lerp(transform.localScale, ObjScale, fraction);

            yield return null;
        }

        transform.position = Objposition;
        transform.localScale = ObjScale;

        yield return new WaitForSeconds((float)0.01);

        transform.position = orignalPos;
        transform.localScale = orignalSca;

    }


    public void startWave()
    {
        StartCoroutine(MoveAndScale());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            StartCoroutine(MoveAndScale());
        }
        FirstPersonController playerController = other.GetComponent<FirstPersonController>();

        if (playerController != null)
        {
            // Disable the player's movement
            StartCoroutine(DisableMovemant(playerController, 1f));
            Debug.Log("Player movement disable by Wave");
        }


        if (other.CompareTag("Player"))
        {
            Player_Health playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(waveDmg);
            }
        }
    }

    private IEnumerator DisableMovemant(FirstPersonController pControl, float longTime)
    {
        pControl.SetMovement(false);
        Debug.Log("Player movement disable");

        yield return new WaitForSeconds(longTime);

        pControl.SetMovement(true);
        Debug.Log("Player movement disabled!");

    }

   

}

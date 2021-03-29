using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manatee : MonoBehaviour
{
    [SerializeField]
    float bounceSpeed = 3;
    [SerializeField]
    float bounceHeight = 5f;
    [SerializeField]
    [Range(0.0f, 2 * Mathf.PI)]
    float bounceOffset = 0;

    [SerializeField]
    GameObject heartPrefab;         //set in inspector
    [SerializeField]
    Vector3 offset = new Vector3(-6.9f, 17.45f, -3f);
    [SerializeField]
    AudioClip sHeartNoise;
    [SerializeField]
    float destroyDelay = 5;
    AudioSource fx;
    GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        fx = GameObject.FindGameObjectWithTag("FX").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        transform.position = new Vector3(pos.x, pos.y + Mathf.Sin(bounceOffset + Time.time * bounceSpeed) * bounceHeight, pos.z);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            heart = Instantiate(heartPrefab);
            Vector3 placement = this.transform.position + offset;
            heart.transform.position = placement;
            fx.PlayOneShot(sHeartNoise);
            

        }
    }

    void OnTriggerExit(Collider col)
    {
        StartCoroutine(EndManatee(heart));
    }

    private IEnumerator EndManatee(GameObject heart)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(heart);
        Destroy(this.gameObject);
    }
}

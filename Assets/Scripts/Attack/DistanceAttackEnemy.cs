using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAttackEnemy : MonoBehaviour {

	public GameObject projectilePrefab;

    public float recarga = 1.5f;

	public float attack = 15;

    private bool disparo;
    private float time;

    // Use this for initialization
    void Start()
    {
        disparo = false;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
		if (disparo && time > recarga)
        {
			GameObject pr = Instantiate(projectilePrefab, this.transform.parent);
			pr.transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
			pr.GetComponent<ArmaArrojadiza>().setWaponAttack(this.attack);
			disparo = false;
			time = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !disparo)
        {
            disparo = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !disparo)
        {
                disparo = true;
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		disparo = false;
	}

}

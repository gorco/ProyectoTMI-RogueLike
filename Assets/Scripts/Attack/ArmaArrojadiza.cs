using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaArrojadiza : MonoBehaviour {
    public float velocidad = 5.0F;

    private LifeHero heroLife;

	[SerializeField]
	private int weaponAttack = 0;
    public float weaponModifier = 1;

    private Vector2 posIni;
	private Vector2 posEnd;
	private Vector2 vector;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find("Player");
		heroLife = player.GetComponent<LifeHero>();

		this.posEnd = new Vector2(heroLife.transform.localPosition.x, heroLife.transform.localPosition.y);
        posIni = transform.localPosition;

		vector = posEnd - posIni;
		this.GetComponent<Rigidbody2D>().velocity = vector.normalized * velocidad;
	}
	
	// Update is called once per frame
	void Update () {
        
    }
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player" || collision.gameObject.tag == "muro")
		{
			if (collision.gameObject.tag == "Player")
				heroLife.receiveAttack(weaponAttack);

			Destroy(this.gameObject);
		}
	}

    public void setWaponAttack(float shooterAttack)
    {
		this.weaponAttack = Mathf.CeilToInt(shooterAttack * weaponModifier);
    }
}

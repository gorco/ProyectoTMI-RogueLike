using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDoor : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	public int sceneToGo = 0;
	private Animator anim;
	public GameObject player;
	public Sprite playerSprite;
	private Sprite playerStart;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		if(player)
			playerStart = player.GetComponent<Image>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		SceneManager.LoadScene(sceneToGo);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		anim.SetTrigger("open");
		if(player)
			player.GetComponent<Image>().sprite = playerSprite;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		anim.SetTrigger("close");
		if(player)
			player.GetComponent<Image>().sprite = playerStart;
	}
}

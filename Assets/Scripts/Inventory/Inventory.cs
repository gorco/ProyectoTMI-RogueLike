using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

	public static Inventory Inv;

	private List<Slot> slotsList = new List<Slot>();
	private List<Slot> equipmentSlots = new List<Slot>();

	[Header("Equipment Slots")]
	private Slot helmet;
	private Slot right;
	private Slot left;
	private Slot body;
	private Slot foots;

	[Header("Hover")]
	public GameObject iconPrefab;
	public float topPad;
	private static GameObject hoverObject;

	[Header("HUD")]
	public Canvas canvas;
	public CanvasGroup canvasGroup;
	public EventSystem eventSystem;

	public GameObject tooltipObject;
	public Text sizeTextObject;
	public Text visualTextObject;

	private static GameObject tooltip;
	private static Text sizeText;
	private static Text visualText;

	private static Slot from, to;
	private int emptySlots = 0;

	[Header("Potions")]
	public GameObject[] potionsTiles;

	[Header("Onjects Level 1-5")]
	public GameObject[] consumibleTiles1;                           //Array of consumible prefabs.
	public GameObject[] equipmentTiles1;                            //Array of equipment prefabs.
	public GameObject[] weaponsTiles1;                              //Array of weapons prefabs.

	[Header("Onjects Level 6-10")]
	public GameObject[] consumibleTiles2;                           //Array of consumible prefabs.
	public GameObject[] equipmentTiles2;                            //Array of equipment prefabs.
	public GameObject[] weaponsTiles2;                              //Array of weapons prefabs.

	[Header("Onjects Level 11-15")]
	public GameObject[] consumibleTiles3;                           //Array of consumible prefabs.
	public GameObject[] equipmentTiles3;                            //Array of equipment prefabs.
	public GameObject[] weaponsTiles3;                              //Array of weapons prefabs.

	[Header("Onjects Level 16-20")]
	public GameObject[] consumibleTiles4;                           //Array of consumible prefabs.
	public GameObject[] equipmentTiles4;                            //Array of equipment prefabs.
	public GameObject[] weaponsTiles4;                              //Array of weapons prefabs.

	[Header("Onjects Level 21-25")]
	public GameObject[] consumibleTiles5;                           //Array of consumible prefabs.
	public GameObject[] equipmentTiles5;                            //Array of equipment prefabs.
	public GameObject[] weaponsTiles5;                              //Array of weapons prefabs.

	void Awake ()
	{
		Inv = this;

		tooltip = tooltipObject;
		sizeText = sizeTextObject;
		visualText = visualTextObject;

		slotsList.Clear();
		foreach(Slot slt in GetComponentsInChildren<Slot>())
		{
			if(slt != helmet && 
				slt != body && 
				slt != right && 
				slt != left && 
				slt != foots)
			{
				slotsList.Add(slt);
				emptySlots++;
			} else
			{
				equipmentSlots.Add(slt);
			}
		}

    }

	void Start()
	{
		OpenInventory(false);
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			//Remove/drop items if clicking out of inventory and item is selected
			if (!eventSystem.IsPointerOverGameObject(-1) && from != null)
			{
				if (from.IsSpecialized())
				{
					from.RemoveItem();
					CalcStats();
				} else
				{
					from.RemoveItem();
					IncreaseEmptyCount();
				}
				Destroy(hoverObject);
				ResetInventoryState();
			}
		}

		// Move hover object around the canvas
		if (hoverObject != null)
		{
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.GetComponent<Camera>(), out position);
			position.x += 1;
			position.y += 1;
			hoverObject.transform.position = canvas.transform.TransformPoint(position);
		}
	}

	public void ShowTooltip(GameObject slot)
	{
		Slot tmpSlot = slot.GetComponent<Slot>();
		
		if(this.enabled == true && !tmpSlot.IsEmpty() && hoverObject == null)
		{
			visualText.text = tmpSlot.GetCurrentItem().GetTooltip();
			sizeText.text = visualText.text;

			tooltip.SetActive(true);

			float xPos = slot.transform.position.x + 1;
			float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - topPad;

			tooltip.transform.position = new Vector2(xPos, yPos);
		}
		
	}

	public void HideTooltip()
	{
		tooltip.gameObject.SetActive(false);
	}

	public bool AddItem(Item item)
	{
		if(emptySlots > 0)
		{
			foreach(Slot slot in slotsList)
			{
				if (slot.IsEmpty())
				{
					slot.AddItem(item);
					DecreaseEmptyCount();
					return true;
				}
			}
		}

		return false;
	}

	public void DecreaseEmptyCount()
	{
		emptySlots--;
	}

	public void IncreaseEmptyCount()
	{
		emptySlots++;
	}

	private Slot GetSpecilizedSlot(ItemType type)
	{
		foreach (Slot slot in equipmentSlots)
		{
			if(slot.GetSpecialization() == type)
			{
				return slot;
			}
		}
		return null;
	}

	public void EquipItem(Slot slot)
	{
		Slot spSlot = GetSpecilizedSlot(slot.GetCurrentItem().itemType);
        MoveItem(slot.gameObject);
		MoveItem(spSlot.gameObject);
	}

	public void MoveItem(GameObject clicked)
	{
		if (from == null)
		{
			if (!clicked.GetComponent<Slot>().IsEmpty())
			{
				from = clicked.GetComponent<Slot>();
				from.GetComponent<Image>().color = Color.gray;

				hoverObject = Instantiate(iconPrefab);
				hoverObject.GetComponent<Image>().sprite = from.GetCurrentItem().itemSprite;
				from.HideItem(true); 

				RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
				RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

				hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
				hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

				hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
				hoverObject.transform.localScale = from.gameObject.transform.localScale;
			}
		}

		else if (to == null)
		{
			to = clicked.GetComponent<Slot>();
			Destroy(hoverObject);
		}

		if(to!=null && from != null)
		{
			if(to.IsSpecialized() && to.IsEmpty())
			{
				IncreaseEmptyCount();
			} else if(from.IsSpecialized() && to.IsEmpty())
			{
				DecreaseEmptyCount();
			}

			Item tmp = to.GetCurrentItem();
			if (to.ChangeItem(from.GetCurrentItem()))
			{
				from.ChangeItem(tmp);
			}

			if (!from.IsEmpty())
			{
				from.HideItem(false);
			} 

			if (to.IsSpecialized() || from.IsSpecialized())
			{
				CalcStats();
			}

			ResetInventoryState();
		}
	}

	private void ResetInventoryState()
	{
		from.GetComponent<Image>().color = Color.white;
		to = null;
		from = null;
		hoverObject = null;
	}

	public void OpenInventory(bool open)
	{
		if (open && canvasGroup.alpha < 0.5f)
		{
			this.enabled = true;
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		} else if(!open && canvasGroup.alpha >= 0.5f)
		{
			this.enabled = false;
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}
	}

	public void updateStatsText(string text)
	{
		//statsText.text = text;
	}

	public void CalcStats()
	{
		//Player p = FindObjectOfType<Player>();
		int maxLife = 0;
		int str = 0;
		int def = 0;
		int dex = 0;
		int spd = 0;
		int luc = 0;

		for (int i = 0; i < equipmentSlots.Count; i++)
		{
			if (!equipmentSlots[i].IsEmpty())
			{
				Item item = equipmentSlots[i].GetCurrentItem();
				maxLife += item.maxLife;
				str += item.str;
				def += item.def;
				dex += item.dex;
				spd += item.spd;
				luc += item.luc;
			}
		}

		//p.SetStats(maxLife, str, def, dex, spd, luc);
	}

	public void SaveInventory()
	{
		//Save Items
		string content = string.Empty;
		for (int i = 0; i < slotsList.Count; i++)
		{
			if (!slotsList[i].IsEmpty())
			{
				Item item = slotsList[i].GetCurrentItem();
				//[0] = slotPosition
				//[1] = itemName
				//[2] = itemType
				//[3] = itemPower
				content += i + "/" + item.itemName + "/" + item.itemType.ToString() + "/" + item.power + ";";
			}
			else
			{
				content += i + "/" + "empty;";
			}
		}

		//Save Equipment
		string equipment = string.Empty;
		for (int i = 0; i < equipmentSlots.Count; i++)
		{
			if (!equipmentSlots[i].IsEmpty())
			{
				Item item = equipmentSlots[i].GetCurrentItem();
				//[0] = slotPosition
				//[1] = itemName
				//[2] = itemType
				//[3] = itemPower
				equipment += i + "/" + item.itemName + "/" + item.itemType.ToString() + "/" + item.power + ";";
			}
			else
			{
				equipment += i + "/" + "empty;";
			}
		}

		Debug.Log("SAVE EQUIP " + equipment);
		Debug.Log("SAVE INV " + content);
        PlayerPrefs.SetString("InventoryContent", content);
		PlayerPrefs.SetString("EquipmentContent", equipment);
		PlayerPrefs.Save();
	}

	public void LoadInventory()
	{
		
		//Load Equipment
		string equipment = PlayerPrefs.GetString("EquipmentContent");
		if(equipment != string.Empty)
		{
			string[] splitEquipment = equipment.Split(';');
			foreach(string savedItem in splitEquipment)
			{
				if (!string.IsNullOrEmpty(savedItem))
				{
					string[] splitValues = savedItem.Split('/');
					if (!splitValues[1].Equals("empty"))
					{
						Item item = GetItemInstance(splitValues[1], splitValues[2], int.Parse(splitValues[3]));
						if (item != null)
						{
							equipmentSlots[int.Parse(splitValues[0])].AddItem(item);
							item.gameObject.SetActive(false);
						}
					}
				}
			}
		}

		//Load Items
		string content = PlayerPrefs.GetString("InventoryContent");
		if (content != string.Empty)
		{
			string[] splitContent = content.Split(';');
			foreach (string savedItem in splitContent)
			{
				if (!string.IsNullOrEmpty(savedItem))
				{
					string[] splitValues = savedItem.Split('/');
					if (splitValues[1]!="empty")
					{
						Item item = GetItemInstance(splitValues[1], splitValues[2], int.Parse(splitValues[3]));
						if (item != null)
						{
							slotsList[int.Parse(splitValues[0])].AddItem(item);
							item.gameObject.SetActive(false);
						}
					} 
				}
			}
		}
		Debug.Log("LOAD EQUIP " + equipment);
		Debug.Log("LOAD INV " + content);
		CalcStats();
	}

	//Return an instance of object with the name, type and power passed as arguments
	private Item GetItemInstance(string name, string type, int power)
	{
		if (type.Equals(ItemType.Potions.ToString()))
		{
			foreach (GameObject item in potionsTiles)
			{
				if (item.GetComponent<Item>().itemName.Equals(name))
				{
					return Instantiate<GameObject>(item).GetComponent<Item>();
				}
			}
			return null;
		}

		switch (power)
		{
			case 1:
				if (type.Equals(ItemType.Consumable.ToString()))
				{
					foreach (GameObject item in consumibleTiles1)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else if (type.Equals(ItemType.Weapon.ToString()) || type.Equals(ItemType.Shield.ToString()))
				{
					foreach (GameObject item in weaponsTiles1)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else //Equipment
				{
					foreach (GameObject item in equipmentTiles1)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				break;
			case 2:
				if (type.Equals(ItemType.Consumable.ToString()))
				{
					foreach (GameObject item in consumibleTiles2)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else if (type.Equals(ItemType.Weapon.ToString()) || type.Equals(ItemType.Shield.ToString()))
				{
					foreach (GameObject item in weaponsTiles2)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else //Equipment
				{
					foreach (GameObject item in equipmentTiles2)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				break;
			case 3:
				if (type.Equals(ItemType.Consumable.ToString()))
				{
					foreach (GameObject item in consumibleTiles3)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else if (type.Equals(ItemType.Weapon.ToString()) || type.Equals(ItemType.Shield.ToString()))
				{
					foreach (GameObject item in weaponsTiles3)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else //Equipment
				{
					foreach (GameObject item in equipmentTiles3)
					{
						if (item.GetComponent<Item>().itemName.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				break;
			case 4:
				if (type.Equals(ItemType.Consumable.ToString()))
				{
					foreach (GameObject item in consumibleTiles4)
					{
						if (item.name.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else if (type.Equals(ItemType.Weapon.ToString()) || type.Equals(ItemType.Shield.ToString()))
				{
					foreach (GameObject item in weaponsTiles4)
					{
						if (item.name.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else //Equipment
				{
					foreach (GameObject item in equipmentTiles4)
					{
						if (item.name.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				break;
			case 5:
				if (type.Equals(ItemType.Consumable.ToString()))
				{
					foreach (GameObject item in consumibleTiles5)
					{
						if (item.name.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else if (type.Equals(ItemType.Weapon.ToString()) || type.Equals(ItemType.Shield.ToString()))
				{
					foreach (GameObject item in weaponsTiles5)
					{
						if (item.name.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				else //Equipment
				{
					foreach (GameObject item in equipmentTiles5)
					{
						if (item.name.Equals(name))
						{
							return Instantiate<GameObject>(item).GetComponent<Item>();
						}
					}
				}
				break;
			default:
				break;
		}
		return null;
	}
}

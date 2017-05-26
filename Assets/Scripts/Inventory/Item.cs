using UnityEngine;

public enum ItemType
{
	Head,
	Armor,
	Gloves,
	Boots,
	Weapon,
	Shield,
	Consumable,
	Potions
}

public enum Quality
{
	Common, 
	Uncommon, 
	Rare,
	Epic
}

public class Item : MonoBehaviour {

	[Header("Item icon")]
	public Sprite itemSprite;

	[Header("Item type")]
	public ItemType itemType;

	[Header("Item name and info")]
	public string itemName;
	public string itemInfo;

	[Header("Level of power and rarity")]
	public int power;
	public Quality quality;

	[Header("Price")]
	public int value;

	[Header("Attributes")]
	public int life;
	public int maxLife;
	public int atk;
	public float spdAtk;
	public int def;
	public float luc;
	public float weight;

	public void Use ()
	{
		switch (itemType)
		{
			case ItemType.Consumable:
				LifeHero p = GameObject.Find("Player").GetComponent<LifeHero>();
				p.ObtainLife(life);
				break;
			case ItemType.Potions:
				GameObject.Find("Player").GetComponent<LifeHero>().ObtainLife(life);
				break;
			default:
				break;
		}
	}

	public bool CanBeEquiped()
	{
		if (itemType != ItemType.Consumable && itemType != ItemType.Potions)
		{
			return true;
		}

		return false;
	}

	public bool CanBeConsumed()
	{
		if (itemType == ItemType.Consumable || itemType == ItemType.Potions)
		{
			return true;
		}

		return false;
	}

	public bool CanBeUsed()
	{
		return false;
	}

	public string GetTooltip()
	{
		string stats = string.Empty;
		string color = string.Empty;
		string newLine = string.Empty;

		if(itemInfo != string.Empty)
		{
			newLine = "\n";
		}

		switch (quality)
		{
			case Quality.Common:
				color = "white";
				break;
			case Quality.Uncommon:
				color = "green";
				break;
			case Quality.Rare:
				color = "cyan";
				break;
			case Quality.Epic:
				color = "orange";
				break;
		}

		stats += "\n";
		if (life > 0)
		{
			stats += "\n\tHeal " + life.ToString() + " points of life";
		}
		if (maxLife > 0)
		{
			stats += "\n\t+" + maxLife.ToString() + " Max Life"; 
		}
		if (atk > 0)
		{
			stats += "\n\t+" + atk.ToString() + " Attack Power";
		}
		if (spdAtk > 0)
		{
			stats += "\n\t+" + spdAtk.ToString() + " Attack Speed";
		}
		if (def > 0)
		{
			stats += "\n\t+" + def.ToString() + " Defense Bonus";
		}
		if (luc > 0)
		{
			stats += "\n\t+" + luc.ToString() + " Luck (critical attack)";
		}
		if (weight > 0)
		{
			stats += "\n\t+" + weight.ToString() + " Kg of Weight";
		}
		return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>"+newLine+"{1}</color></i>{2}</size>",itemName,itemInfo,stats);
	}

}

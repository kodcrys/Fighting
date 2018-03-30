using UnityEngine;

public enum TypeObject {hat, weapon, tshirt}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Data/Equipment")]
public class DataItems : ScriptableObject {

	public int id;

	public new string name;

	public TypeObject typeItem;

	public Sprite avatar;

	public int ATK;
	public int DEF;
	public int HP;

	public bool isOwned;
}

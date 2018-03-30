using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Data/Character")]
public class DataCharacter : ScriptableObject {

	public int id;
	public new string name;
	public Sprite equipmentOfChar;
	public Sprite equipmentWP;
	public Sprite equipmetnTShirt;
	public int ATK;
	public int DEF;
	public int HP;
	public bool isOwned;
}

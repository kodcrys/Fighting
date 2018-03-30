using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Data/Quest")]
public class DataQuests : ScriptableObject {
	new public int idQuest = 0;
	public Sprite icon;
	public string content = "";
	public int requirement = 0;
	public int doing = 0;
	public int rewardGold = 0;
	public int rewardExp = 0;

	// Show btn can claim reward
	public bool isDone = false;


}

using UnityEngine;

[CreateAssetMenu(fileName = "New Level Stat", menuName = "Data/LevelStat")]
public class DataLevelStat : ScriptableObject {

	public new int id = 0;
	public int level = 0;
	public int[] expLevelUp;

	public int hpBonus;
	public int atkBonus;
	public int defBonus;
}

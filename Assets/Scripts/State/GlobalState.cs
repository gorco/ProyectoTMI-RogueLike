public class GlobalState : IState
{
	public static int RedEnemies {	get; set; }

	public static int BlueEnemies { get; set; }

	public static int RandomNum { get; set; }

	protected static GlobalState instance;
	public static GlobalState Instance {  get { return instance == null ? instance = new GlobalState() : instance; } }

	public int RedEnemiesStatic
	{
		get { return RedEnemies; }
	}

	public int BlueEnemiesStatic
	{
		get { return BlueEnemies; }
	}

	public int RandomNumStatic
	{
		get { return RandomNum; }
	}

}

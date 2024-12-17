using Avalonia.Input;
using Digger.Architecture;

namespace Digger;

public static class Game
{
	private const string mapWithPlayerTerrain = @"
TTT T
TTP T
T T T
TT TT";

	private const string mapWithPlayerTerrainSackGold = @"
TTS SSGP TTTTS
TST  TSTT TTTT
TTTTTTSTT TTTT
T TSTS TT TTTS
T TTTG ST TTTT
TSTSTT TT TTTT" ;

	private const string mapWithPlayerTerrainSackGoldMonster = @"
PTTGTT TST
TST  TSTTM
TTT TTSTTT
T TSTS TTT
T TTTGM T 
P M  M   M
TSTSTTMTTT
S TTST  TG
 TGST MTTT
 T  TMTTTT";

	private const string mapWithSmallTest = @"
TTTTTTTTTTTTTTTTTTTT
TP        G       MT
TTTTTTTTTTTTTTTTTTTT";

	private const string testMapWithFallingSackOnMonsersHead = @"
TTT
TST
T T
T T
T T
TMT
TTT";

	private const string testMapMonserIsNotWalking = @"
T TT
TPTT
T TT
T TT
T MT
TTTT";
	private const string huuuuuugeMap = @"
TTT TST TTT TTT TTM TTT TTT TTT TTT TTT TTT TTT TTT TTT
TST TTT TPT TST TTT TT TTT TTTSTTT TTT TTT TTT TTT TTT 
TTT TTT TTT TTT TTT TTT TTT TTTSTTT TTTSTTTSTTT TTT TTT
T TTSGTTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT T
TTT TTT TST TTT TTT TTT TTTMTTT TTT TTT TTT TTT TTT TTT
T TTT TTT TTT TTT TTB TTT BTT TTT TTT TTTSTTT TTTSTTT T
T TTT TTT TTT STT BTT TTT TBT TTT TTT TTT TTT TTT TTT T
TST TTT TTT TTS TTT TTT TTTBTTT TTT TTT TTT TTT STT TTT
T TTT TTT TTT TTT TTT TTT TTT TST TTTGTTT TTT TTT TTT T
TTT SSS TTT TTT TTM TTT TTT TTT TTT TTT TTT TTT TTT TTT
TTT TTT TTT TTT TTB TTT TTTGTTT TTT TTT TTT TTT TTT TTT
T TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT T
TTT TTT TTT TTT TTB TTT TTT TTT TTT TTT TTT TTT TTT TTT
T TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT TTT T
T TTT TTT TTT TTT TTT TTT MTT TTT TTT TTT TTT TTT TTT T";
	
	public static ICreature?[,] Map;
	public static int Scores;
	public static bool IsOver;

	public static Key KeyPressed;
	public static int MapWidth => Map.GetLength(0);
	public static int MapHeight => Map.GetLength(1);

	public static void CreateMap()
	{
		Map = CreatureMapCreator.CreateMap(huuuuuugeMap);
	}
}
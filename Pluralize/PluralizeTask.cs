namespace Pluralize;

public static class PluralizeTask
{
	public static string PluralizeRubles(int count)
	{
		if ((count % 10 == 1) && (count % 100 != 11)) 
			return "рубль";
		if (((count % 10 >=2) && (count % 10 <= 4) && (count % 100 - count % 10 != 10)))
			return "рубля";
		return "рублей";
	}
}        
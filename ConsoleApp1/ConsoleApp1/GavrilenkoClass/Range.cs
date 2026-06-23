//хранение диапазона чисел — макс и мин значения, обединили в один класс int MIN_INPUT_NUMBER-int MAX_INPUT_NUMBER
//используется в InputService.cs, NumberRandom.cs, GameGavrilenko.cs
public class Range
{
    public int Min { get; } 
    public int Max { get; }

    public Range(int min, int max)
    {
        Min = min;
        Max = max;
    }
}
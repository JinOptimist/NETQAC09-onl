namespace BykiKorovy;

public class GuessEvaluator
{
    public CheckResult Evaluate(string secret, string guess)
    {
        var result = new CheckResult();

        for (int i = 0; i < secret.Length; i++)
        {
            if (secret[i] == guess[i])
            {
                result.Bulls++;
            }
            else if (secret.Contains(guess[i]))
            {
                result.Cows++;
            }
        }

        return result;
    }
}
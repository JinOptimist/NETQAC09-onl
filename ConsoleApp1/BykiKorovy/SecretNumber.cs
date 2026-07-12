namespace BykiKorovy
{
    public class SecretNumber
    {
        public string Generate(int? seed)
        {
            if (seed == null)
            {
                seed = DateTime.Now.Millisecond;
            }
            var random = new Random(seed.Value);

            string secret = "";

            while (secret.Length < 4)
            {
                int digit = random.Next(0, 10);

                if (!secret.Contains(digit.ToString()))
                {
                    secret += digit;
                }
            }

            return secret;
        }
    }
}




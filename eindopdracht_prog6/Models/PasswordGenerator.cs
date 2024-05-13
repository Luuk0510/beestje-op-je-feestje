namespace BeestjeOpJeFeestje.ASP.Models
{
    public class PasswordGenerator
    {
        private const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        private const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Digits = "0123456789";
        private const string SpecialChars = "!@#$%^&*()-_+=";

        public static string GenerateRandomPassword(int length)
        {
            string allChars = LowerCase + UpperCase + Digits + SpecialChars;
            Random random = new Random();

            string password = new string(new char[] {
            LowerCase[random.Next(LowerCase.Length)],
            UpperCase[random.Next(UpperCase.Length)],
            Digits[random.Next(Digits.Length)],
            SpecialChars[random.Next(SpecialChars.Length)]
        });

            password += new string(Enumerable.Repeat(allChars, length - 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            password = new string(password.ToCharArray().OrderBy(s => (random.Next(2) % 2) == 0).ToArray());

            return password;
        }
    }
}

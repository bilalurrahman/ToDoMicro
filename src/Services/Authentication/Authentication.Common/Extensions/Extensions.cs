


namespace Authentication.Common.Extensions
{
    public static class Extensions
    {
     
        public static string HashValues(string value)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(value, 10);
            return hashed;
        }

        public static bool VerifyHashedValues(this string from, string to)
        {
            return BCrypt.Net.BCrypt.Verify(from, to);
        }
    }
}

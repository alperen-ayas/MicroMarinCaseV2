namespace MicroMarinCaseV2.Domain.SeedWorks
{
    public static class Guard
    {
        public static void CannotNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{parameterName} can not be 'null'.");
            }
        }
    }
}

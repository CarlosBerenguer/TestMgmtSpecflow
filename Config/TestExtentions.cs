namespace TestJamaTestMgmt.Config
{
    public static class TestExtentions
    {
        public static int OnlyNumbers(this string value)
        {
            //Remember to Review -Not Sure if it's right
            return Convert.ToInt16(new string(value.Where(char.IsDigit).ToArray()));
        }
    }
}

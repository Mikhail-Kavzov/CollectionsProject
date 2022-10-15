namespace CollectionsProject.Converter
{
    public static class TimeConverter
    {
        public static DateTime ConvertFromUTCTime(string Time)
        {
            if (!DateTime.TryParse(Time, out DateTime time))
                throw new ArgumentException("DateTime is not correct");
            return time;
        }
    }
}

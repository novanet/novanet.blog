namespace AlertHandler.Facade.ApplicationInsights
{
    public static class LogAlertParser
    {
        public static string LinkToSearchResults(this LogAlert alert)
        {
            var allOf = alert.data.alertContext.condition.allOf[0];
            return allOf.linkToSearchResultsUI.Replace("\n", "");
        }

        public static string? ExceptionMessage(this LogAlert alert)
        {
            var dimension = alert!.GetDimension("outerMessage");

            if (dimension is not null && dimension.Contains("\n"))
                dimension = dimension.Split("\n")[0];

            return dimension;
        }

        public static string? CloudRoleName(this LogAlert alert)
            => alert!.GetDimension("cloud_RoleName");

        private static string? GetDimension(this LogAlert alert, string dimensionName)
            => alert!.data.alertContext.condition.allOf[0].dimensions.FirstOrDefault(x => x.name == dimensionName)?.value;
    }
}

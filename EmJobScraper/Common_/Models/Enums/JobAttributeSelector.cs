namespace Common.Models.Enums
{
    public static class JobAttributeSelector
    {
        public static string HtmlSelector(this JobAttribute attribute)
        {
            switch (attribute)
            {
                case JobAttribute.Link:
                    return "href";
            }
            throw new NotImplementedException();
        }
    }
}

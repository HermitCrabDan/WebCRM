namespace WebCRM.Helpers
{
    using System.Text;
    using System.Collections.Generic;
    using System.Web;
    /// <summary>
    /// Helper class to provide a static method to make user input safer
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class XSSFilterHelper
    {
        public static string FilterForXSS(string userInputString)
        {
            if (!string.IsNullOrWhiteSpace(userInputString))
            {
                return HttpUtility
                    .JavaScriptStringEncode(
                        userInputString
                        .Replace("<","&lt;")
                        .Replace(">","&gt;")
                        );
            }
            return string.Empty;
        }
    }
}

using System.ComponentModel;

namespace BackgroundApp
{
    /// <summary>
    /// Language code used for papago ui url.
    /// </summary>
    public enum LanguageCode
    {
        [Description("ko")]
        KOREAN,
        [Description("en")]
        ENGLISH,
        [Description("ja")]
        JAPANESE,
        [Description("zh-CN")]
        CHINESE_CN,
        [Description("zh-TW")]
        CHINESE_TW,
        [Description("es")]
        SPAINSH,
        [Description("fr")]
        FRANCH,
        [Description("de")]
        GERMAN,
        [Description("ru")]
        RUSSIAN,
        [Description("pt")]
        PORTUGUESE,
        [Description("it")]
        ITALIAN,
        [Description("vi")]
        VIETNAMESE,
        [Description("th")]
        THAI,
        [Description("id")]
        INDONESIAN,
        [Description("hi")]
        HINDI
    };

    /// <summary>
    /// Language code helper methods.
    /// </summary>
    public static class LanguageCodeExtensions
    {
        /// <summary>
        /// Convert the enum type to actual code.
        /// </summary>
        /// <param name="code">Language in English</param>
        /// <returns>Code for papago url</returns>
        public static string ToLanguageCode(this LanguageCode code)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])code
                .GetType().GetField(code.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}

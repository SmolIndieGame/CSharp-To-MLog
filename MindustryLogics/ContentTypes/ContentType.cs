namespace MindustryLogics
{
    /// <summary>
    /// This is the base class for all content types.
    /// </summary>
    public abstract class ContentType
    {
    }

    /// <summary>
    /// This class specifies that the names of types contained in its subclasses should be converted to kebab case.<br/>
    /// Example:<br/>
    /// ExAm_Ple will be converted to @exam-ple.
    /// </summary>
    public abstract class KebabCaseType : ContentType
    {
    }

    /// <summary>
    /// This class specifies that the names of types contained in its subclasses should be converted to lower camel case.<br/>
    /// Example:<br/>
    /// ExAmPle will be converted to @exAmPle.
    /// </summary>
    public abstract class CamelCaseType : ContentType
    {
    }
}

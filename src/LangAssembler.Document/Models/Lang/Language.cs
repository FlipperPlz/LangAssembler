using System.Text;
using LangAssembler.Document.Extensions;
using LangAssembler.Document.Models.Source;
using Microsoft.Extensions.Logging;
// ReSharper disable PublicConstructorInAbstractClass

namespace LangAssembler.Document.Models.Lang;

public abstract class Language 
{
    #region static
    private static readonly Dictionary<Type, Language> Languages = new Dictionary<Type, Language>();
    public static readonly Language PlainTextLanguage = Of<PlainText>();
    public static readonly Language BinaryLanguage = Of<Binary>();

    public static Language Of<TLang>() where TLang : Language, new()
    {
        var type = typeof(TLang);
        if(Languages.TryGetValue(type, out var language))
        {
            return language;
        }

        var lang = new TLang();
        Languages.Add(type, lang);
        return lang;
    }

    #endregion
    public abstract string LanguageName { get; }
    public abstract string LanguageAbbreviation { get; }
    public virtual Encoding LanguageEncoding => Encoding.UTF8;
    public ILogger<Language>? LanguageLogger { get; }

    protected Language(ILogger<Language>? logger = default)
    {
        LanguageLogger = logger;
    }

    public static Document OpenDocument<T>(IDocumentSource source, Encoding? encoding = null) where T: Language, new() => 
        Document.Of<T>(source, encoding);
    
    public class PlainText : Language
    {
        public const string Name = "PlainText", Abbreviation = "txt";
        public override string LanguageName => Name;
        public override string LanguageAbbreviation => Abbreviation;
    }
    
    public class Binary : Language
    {
        public const string Name = "Binary", Abbreviation = "bin";
        public override string LanguageName => Name;
        public override string LanguageAbbreviation => Abbreviation;
    }
    
}
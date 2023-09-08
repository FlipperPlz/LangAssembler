using System.Text;
using LangAssembler.Models.Doc;
using LangAssembler.Models.Doc.Source;
using Microsoft.Extensions.Logging;

// ReSharper disable PublicConstructorInAbstractClass

namespace LangAssembler.Models.Lang;

public abstract class Language 
{
    #region static
    private static readonly Dictionary<Type, Language> Languages = new Dictionary<Type, Language>();
    public static readonly Language PlainTextLanguage = Of<PlainText>();
    public static readonly Language BinaryLanguage = Of<Binary>();

    public static Language Of<TLang>() where TLang : Language, new()
    {
        var type = typeof(TLang);
        if(!Languages.TryGetValue(type, out var language))
        {
            var lang = new TLang();
            Languages.Add(type, lang);
            language = lang;
        }

        return language;
    }

    #endregion
    public abstract string LanguageName { get; }
    public abstract string LanguageAbbreviation { get; }
    public virtual Encoding Encoding => Encoding.UTF8;
    public ILogger<Language>? Logger { get; }

    protected Language(ILogger<Language>? logger = default)
    {
        Logger = logger;
    }

    public static Document OpenDocument<T>(DocumentSource source, Encoding? encoding = null) where T: Language, new() => 
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
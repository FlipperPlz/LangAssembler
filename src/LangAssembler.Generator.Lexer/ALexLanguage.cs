using System.Text;
using LangAssembler.Models.Lang;

namespace LangAssembler.Generator.Lexer;

public class ALexLanguage : Language.PlainText
{
    public override Encoding Encoding => Encoding.UTF8;
    public override string LanguageName => "ALex";
    public override string LanguageAbbreviation => "alex";
}
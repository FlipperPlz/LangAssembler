using System.Text;
using LangAssembler.Models.Doc;
using LangAssembler.Models.Doc.Source;
using LangAssembler.Models.Lang;

namespace LangAssembler.Extensions;

public static class FileInfoExtensions
{
    public static DocumentSource.FileSystem ToDocumentSource(this FileInfo file, bool writable = true) => 
        new(file, FileMode.OpenOrCreate, writable ? FileAccess.ReadWrite : FileAccess.Read);
    
    public static Document ToDocument<TLanguage>(this FileInfo file, Encoding encoding, bool writable = true) 
        where TLanguage : Language, new() => Document.Of<TLanguage>(ToDocumentSource(file, writable), encoding);
}
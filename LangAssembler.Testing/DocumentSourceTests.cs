using System.Text;
using LangAssembler.Models.Doc.Source;
using static NUnit.Framework.Assert;

namespace LangAssembler.Testing;

[TestFixture]
public class DocumentSourceTests
{

    public const string TestData = "Test Doc Test Doc";

    [SetUp]
    public void Setup()
    {
        File.WriteAllText("Test.txt", TestData, Encoding.UTF8);
    }

    [TearDown]
    public void Cleanup()
    {
        File.Delete("Test.txt");
    }
    
    [Test]
    public void TestCreate_ReadonlyFilesystem_DocumentSource()
    {
        var info = new FileInfo("Test.txt");
        using var documentSource = new DocumentSource.FileSystem(info, FileMode.Open, FileAccess.Read);
        
        Assert.Multiple(() =>
        {
            That(documentSource.File, Is.EqualTo(info));
            That(documentSource.Stream, Is.Not.Null);
            That(documentSource.IsVirtual, Is.False);
            That(documentSource.CanWrite, Is.False);
            That(documentSource.CanResize, Is.False);
        });
    }
}
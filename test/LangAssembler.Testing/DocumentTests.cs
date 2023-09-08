using LangAssembler.Models.Doc;
using LangAssembler.Models.Doc.Source;
using Moq;

namespace LangAssembler.Testing;

[TestFixture]
public class DocumentTests
{
    private Mock<DocumentSource> _source;
    private Document _document;

    
    [SetUp]
    public void Setup()
    {
        CreateDocumentSource();
    }

    private void CreateDocumentSource()
    {
    }


    [TearDown]
    public async ValueTask  Cleanup()
    {
        await _document.DisposeAsync();
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.Util;
using Xunit;

namespace SyntaxParser.Tests.Core.SingleDelimiter;

public class FileParserTests
{
    [Fact]
    public async Task Skip_Async()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = new List<CsvSyntax>();
        await foreach (var partialResult in SyntaxParser.ParseFileAsync<CsvSyntax>(file.Path, 1))
        {
            result.Add(partialResult);
        }
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(25, result[1].Age);
    }
    
    [Fact]
    public async Task Skip()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = SyntaxParser.ParseFile<CsvSyntax>(file.Path, 1).ToArray();
        Assert.Equal(2, result.Length);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(25, result[1].Age);
    }

    [Fact]
    public async Task Skip_ToJson_Async()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = await SyntaxParser.ParseFileToJsonAsync<CsvSyntax>(file.Path, 1);
        Assert.Equal("[{\"Name\":\"John\",\"Age\":30},{\"Name\":\"Jane\",\"Age\":25}]", result);
    }
    
    [Fact]
    public async Task Skip_ToJson()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = SyntaxParser.ParseFileToJson<CsvSyntax>(file.Path, 1);
        Assert.Equal("[{\"Name\":\"John\",\"Age\":30},{\"Name\":\"Jane\",\"Age\":25}]", result);
    }
    
    [Fact]
    public async Task Skip_Take_Async()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = new List<CsvSyntax>();
        await foreach (var partialResult in SyntaxParser.ParseFileAsync<CsvSyntax>(file.Path, 1, 1))
        {
            result.Add(partialResult);
        }
        Assert.Single(result);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
    }

    [Fact]
    public async Task Skip_Take()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = SyntaxParser.ParseFile<CsvSyntax>(file.Path, 1, 1).ToArray();
        Assert.Single(result);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
    }

    [Fact]
    public async Task Skip_Take_To_Json()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = SyntaxParser.ParseFileToJson<CsvSyntax>(file.Path, 1, 1);
        Assert.Equal("[{\"Name\":\"John\",\"Age\":30}]", result);
    }
        
    [Fact]
    public async Task Skip_Take_To_Json_Async()
    {
        using var file = await TemporaryFile.InitializeAsync($"Name;Age{Environment.NewLine}John;30{Environment.NewLine}Jane;25{Environment.NewLine}");
        var result = await SyntaxParser.ParseFileToJsonAsync<CsvSyntax>(file.Path, 1, 1);
        Assert.Equal("[{\"Name\":\"John\",\"Age\":30}]", result);
    }
}
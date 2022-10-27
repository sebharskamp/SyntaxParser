# SyntaxParser
For those whom want to parse business understandable, and easy to read commands or delimiter separated documents, e.g. CSV's, in to an C# instance.

Not intended to replace JSON or other data structures for external oriented production software. 
Use it to speed up project Initialization, produce data and workflow in harmony with the business.


## Usage
### SyntaxAttribute
Decorate a class with the SyntaxAttribute.
```csharp
[Syntax($"{nameof(From)}=>{nameof(To)}")]
public class Example
{
	public string From { get; set; }

	public string To { get; set; }
}
```

Parse file or text to instances.
```csharp
var result = SyntaxParser.ParseText<Example>("Rome=>Paris");

// result will be an array, Example[], with the following instances.
//[
// { 
//    "From" : "Rome",
//    "To" : "Paris"
// }
//]
```


Parse file or text to json.
```csharp
var result = SyntaxParser.ParseTextToJson<Example>("Rome=>Paris");

// result will be a json array with the following instances.
//[
// { 
//    "From" : "Rome",
//    "To" : "Paris"
// }
//]
```

### SingleDelimiterAttribute
Decorate a class with the SyntaxAttribute.
```csharp
[SingleDelimterSyntax(";")]
public class CsvExample
{
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime SubscriptionDate { get; set; }
}
```

Parse file or text to instances.
```csharp
var result = SyntaxParser.ParseText<CsvExample>("John Doe;42;24-03-2022");

// result will be an array, CsvExample[], with the following instances.
//[
// { 
//    "Name" : "John Doe",
//    "Age" : 42,
//    "SubscriptionDate" : {24-3-2022 00:00:00}
// }
//]
```


Parse file or text to json.
```csharp
var result = SyntaxParser.ParseTextToJson<CsvExample>("John Doe;42;24-03-2022");

// result will be a json array with the following instances.
//[
// {
//    "Name":"John Doe",
//    "Age": 42,
//    "SubscriptionDate" : "2022-03-24T00:00:00"
// }
//]
```

Methods available are:  
- ParseText
- ParseTextToJson
- ParseFile
- ParseFileAsync 
- ParseFileToJson
- ParseFileToJsonAsync

## Test your syntax
https://regex101.com/r/tCpfu5/1


## Benchmarks
Methods with the name JsonConverter{dataOrigin} are Newtonsoft equivalant of the Parse{dataOrigin}. NewtonSofts converter is added to the benchmarks for reference.
For all methods two tests are run. One small with 4 instructions and one Large with 1344 instructions.


|                 Method |             FilePath |         Mean |      Error |     StdDev |    Gen 0 |   Gen 1 | Allocated |
|----------------------- |--------------------- |-------------:|-----------:|-----------:|---------:|--------:|----------:|
|              ParseText | ./instructions-small |     1.411 μs |  0.0281 μs |  0.0491 μs |   0.1774 |       - |     560 B |
|      JsonConverterText | ./instructions-small |     4.308 μs |  0.0856 μs |  0.1407 μs |   0.2747 |       - |     872 B |
|              ParseFile | ./instructions-small |   128.964 μs |  2.1252 μs |  3.7222 μs |   2.6855 |  1.2207 |   8,864 B |
|      JsonConverterFile | ./instructions-small |   130.865 μs |  2.4706 μs |  2.4265 μs |   1.4648 |  0.7324 |   4,777 B |
| JsonConverterAsyncFile | ./instructions-small |   210.986 μs |  4.1293 μs |  5.6522 μs |   1.7090 |  0.7324 |   5,840 B |
|         ParseAsyncFile | ./instructions-small |   214.532 μs |  4.2618 μs |  8.2110 μs |   3.4180 |  1.7090 |  11,022 B |
|																														  |
|              ParseText | ./instructions-large |   440.304 μs |  8.5135 μs | 13.2545 μs |  44.4336 |       - | 139,920 B |
|              ParseFile | ./instructions-large |   565.516 μs |  3.4166 μs |  2.8530 μs |  56.6406 | 15.6250 | 180,835 B |
|      JsonConverterText | ./instructions-large | 1,175.027 μs | 16.2552 μs | 15.2051 μs |  46.8750 |  3.9063 | 151,713 B |
|         ParseAsyncFile | ./instructions-large | 1,287.300 μs | 10.6269 μs |  9.4205 μs | 164.0625 | 31.2500 | 517,287 B |
|      JsonConverterFile | ./instructions-large | 1,537.124 μs | 30.0269 μs | 54.9059 μs |  48.8281 |  1.9531 | 155,617 B |
| JsonConverterAsyncFile | ./instructions-large | 1,775.679 μs | 32.2867 μs | 48.3253 μs |  46.8750 |  7.8125 | 157,283 B |


## TODO
- Nested Support
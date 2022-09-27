# SyntaxParser
For those whom want to parse business understandable, and easy to read commands in to an C# instance.

Not intended to replace JSON or other data structures for external oriented production software. 
Use it to speed up project Initialization, produce data and workflow inline with the business.


## Usage
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

// result will be an instance with the following values.
// { 
//    "From" : "Rome",
//    "To" : "Paris"
// }
```


Parse file or text to json.
```csharp
var result = SyntaxParser.ParseTextToJson<Example>("Rome=>Paris");

// result will be an instance with the following values.
// { 
//    "From" : "Rome",
//    "To" : "Paris"
// }
```
## Test your syntax
https://regex101.com/r/tCpfu5/1

## TODO
- Nested Support
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

Initiate a new instance of SyntaxParser.
```csharp
var exampleParser = new SyntaxParser<Example>();
```

Parse file or text to instances.
```csharp
var result = exampleParser.ParseText("Rome=>Paris");

// result will be an instance with the following values.
// { 
//    "From" : "Rome",
//    "To" : "Paris"
// }
```

## TODO
- Assembly scanner to enable dependency injection.
- Array Support
- Nested Support
# EnderScript
EnderScript is a simple, lightweight data interchange format which functions similarly to JSON. The name is associated with the goal to _end_ the need for bulky data interchange libraries in small hobby projects.

# Sample
```
//Buffers are for writing data to ES form
ESBuffer buffer = new ESBuffer();
buffer.Add("string", "Hello World!");
buffer.Add("int", -100);
buffer.Add("float", 20.24f);
buffer.Add("bool", true);
buffer.Add("array", new object[] { 1, 2, 3, "Hello", new object[] { "Inner array", 4, 5, 6, false } });

//Writes a existing buffer to a specified location, in this case: sample.es
buffer.WriteToPath("sample.es");

//Builders are for reading text in the ES form and converting it to a data structure
//ES can be read through a path using the ESBuilder.Create function or otherwise can be built
ESBuilder builder = ESBuilder.Create("sample.es");

//ESBuilder.Get retrieves the data.
//The first value is the ES parameter name and the second is the default value in case that param isn't found
var s = builder.Get("string",null); 
var i = builder.Get("int",0);
var f = builder.Get("float",0f);
var b = builder.Get("bool",false);
var a = builder.GetArray("array",null);

// with an string in the ES format using the ESBuilder's constructor
ESBuilder builder2 = new ESBuilder("name|\"Jon Smith\",age|31");
var name = builder2.Get("name",null);
var age = builder2.Get("age", 0);
```

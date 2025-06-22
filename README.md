# ColorHashSharp
Generate color based on the given string. C# port of [ColorHash Javascript Library](https://github.com/zenozeng/color-hash).

[![NuGet version (ColorHashSharp)](https://img.shields.io/nuget/v/ColorHashSharp.svg?style=flat-square)](https://www.nuget.org/packages/ColorHashSharp/) &nbsp; [![NuGet Downloads](https://img.shields.io/nuget/dt/ColorHashSharp.svg)](https://www.nuget.org/packages/ColorHashSharp/)

[![CI](https://github.com/fernandezja/ColorHashSharp/actions/workflows/dotnet-core.yml/badge.svg)](https://github.com/fernandezja/ColorHashSharp/actions/workflows/dotnet-core.yml)&nbsp; [![Build status](https://fernandezja.visualstudio.com/ColorHashSharp/_apis/build/status/ColorHashSharp-CI)](https://fernandezja.visualstudio.com/ColorHashSharp/_build/latest?definitionId=4)

#### Status


|Actions   	|master   	|develop   	|
|---	|---	|---	|
|CI   	|[![CI](https://github.com/fernandezja/ColorHashSharp/actions/workflows/dotnet-core.yml/badge.svg?branch=master)](https://github.com/fernandezja/ColorHashSharp/actions/workflows/dotnet-core.yml)   	|[![CI](https://github.com/fernandezja/ColorHashSharp/actions/workflows/dotnet-core.yml/badge.svg?branch=develop)](https://github.com/fernandezja/ColorHashSharp/actions/workflows/dotnet-core.yml)  	|


#### Basic

```csharp
Debug.WriteLine($"ColorHash >  Hello World");

var colorHash = new ColorHash();

//HSL (return HSL object)
var hsl = colorHash.Hsl("Hello World");

Debug.WriteLine($"  H = {hsl.H} | S = {hsl.S} | L = {hsl.L}");
//  H = 225 | S = 0,35 | L = 0,65


//RGB (return System.Drawing.Color object)
var color = colorHash.Rgb("Hello World");

Debug.WriteLine($"   R= {color.R} | G = {color.G} | B = {color.B}");
//  R= 135 | G = 150 | B = 197    

//Hex (return string)
var hex = colorHash.Hex("Hello World");

Debug.WriteLine($"  hex = {hex}");
//  hex = 8796C5
```

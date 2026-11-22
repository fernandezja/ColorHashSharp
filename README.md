# ColorHashSharp
Generate color based on the given string. C# port of [ColorHash Javascript Library](https://github.com/zenozeng/color-hash).

[![NuGet version (ColorHashSharp)](https://img.shields.io/nuget/v/ColorHashSharp.svg?style=flat-square)](https://www.nuget.org/packages/ColorHashSharp/) &nbsp; [![NuGet Downloads](https://img.shields.io/nuget/dt/ColorHashSharp.svg)](https://www.nuget.org/packages/ColorHashSharp/) &nbsp; [![CodeFactor](https://www.codefactor.io/repository/github/fernandezja/colorhashsharp/badge)](https://www.codefactor.io/repository/github/fernandezja/colorhashsharp)



[![CI](https://github.com/fernandezja/ColorHashSharp/actions/workflows/ci-dotnet.yml/badge.svg)](https://github.com/fernandezja/ColorHashSharp/actions/workflows/ci-dotnet.yml)&nbsp; [![Build status](https://fernandezja.visualstudio.com/ColorHashSharp/_apis/build/status/ColorHashSharp-CI)](https://fernandezja.visualstudio.com/ColorHashSharp/_build/latest?definitionId=4)


#### Status


|Actions   	|main   	|develop   	|
|---	|---	|---	|
|CI   	|[![CI](https://github.com/fernandezja/ColorHashSharp/actions/workflows/ci-dotnet.yml/badge.svg?branch=main)](https://github.com/fernandezja/ColorHashSharp/actions/workflows/ci-dotnet.yml)   	|[![CI](https://github.com/fernandezja/ColorHashSharp/actions/workflows/ci-dotnet.yml/badge.svg?branch=develop)](https://github.com/fernandezja/ColorHashSharp/actions/workflows/ci-dotnet.yml)  	|


#### Basic

```csharp
Debug.WriteLine($"ColorHash >  Hello World");

var colorHash = new ColorHash();

//HSL (return HSL object)
var hsl = colorHash.Hsl("Hello World");

Debug.WriteLine($"  H = {hsl.H} | S = {hsl.S} | L = {hsl.L}");
//  H = 233 | S = 0.35 | L = 0.5


//RGB (return System.Drawing.Color object)
var color = colorHash.Rgb("Hello World");

Debug.WriteLine($"   R= {color.R} | G = {color.G} | B = {color.B}");
//  R = 83 | G = 93 | B = 172

//Hex (return string)
var hex = colorHash.Hex("Hello World");

Debug.WriteLine($"  hex = {hex}");
//  hex = 535DAC
```

#### Hash selection and migration to 2.0

The default hash uses the first four bytes of SHA-256 over UTF-8 as an unsigned
big-endian integer, matching the hash used by JavaScript color-hash 2.0.2.
Color generation still uses the port's HSL calculation and HEX format.

```csharp
var javaScriptBkdr = new ColorHash(new Options { HashFunction = new JavaScriptBkdrHash() });
var historical = new ColorHash(new Options { HashFunction = new BKDRHash() });
```

`JavaScriptBkdrHash` matches the JavaScript `hash: 'bkdr'` hash. Use `BKDRHash`
to reproduce the historical ColorHashSharp colors with the same palettes and
hue ranges. Applications upgrading from NuGet 1.1.0 must recompile and update
`Options.S`, `Options.L`, and protected `GetLS` usage from `ArrayList` to
`List<double>`.

#### Options and null inputs in 2.0

`ColorHash` copies the saturation and lightness palettes and the hue bounds
at construction. To apply changes to `Options`, create a new `ColorHash`
instance. A custom hash instance is used as supplied.

`S` and `L` require at least one finite value between 0 and 1, inclusive.
An empty `HueRanges` list uses the default hue calculation. Each range
requires `0 <= Min <= Max <= 360`. Invalid settings raise `ArgumentException`
at construction. A null input string is treated as empty, including when
passed to a custom hash.

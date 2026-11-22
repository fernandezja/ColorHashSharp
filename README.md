# ColorHashSharp
Generate color based on the given string. C# port of [ColorHash Javascript Library](https://github.com/zenozeng/color-hash).

[![NuGet version (ColorHashSharp)](https://img.shields.io/nuget/v/ColorHashSharp.svg?style=flat-square)](https://www.nuget.org/packages/ColorHashSharp/) &nbsp; [![NuGet Downloads](https://img.shields.io/nuget/dt/ColorHashSharp.svg)](https://www.nuget.org/packages/ColorHashSharp/) &nbsp; 

[![CodeFactor](https://www.codefactor.io/repository/github/fernandezja/colorhashsharp/badge)](https://www.codefactor.io/repository/github/fernandezja/colorhashsharp) [![Codacy Badge](https://app.codacy.com/project/badge/Grade/7b256a3706a2434e9984dfb9f0d3da93)](https://app.codacy.com/gh/fernandezja/ColorHashSharp/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)



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
//  H = 233 | S = 0.5 | L = 0.65


//RGB (return System.Drawing.Color object)
var color = colorHash.Rgb("Hello World");

Debug.WriteLine($"   R= {color.R} | G = {color.G} | B = {color.B}");
//  R = 121 | G = 132 | B = 210

//Hex (return string)
var hex = colorHash.Hex("Hello World");

Debug.WriteLine($"  hex = {hex}");
//  hex = #7984d2
```

#### Hash selection and migration to 2.0

The default hash uses the first four bytes of SHA-256 over UTF-8 as an unsigned
big-endian integer, matching the hash used by JavaScript color-hash 2.0.2.
`Options.UseJavaScriptCompatibility` defaults to `true` and matches
JavaScript's HSL calculation, RGB rounding, and `#rrggbb` format. Set it to
`false` to use the port's historical color calculation and `RRGGBB` format.

```csharp
var javaScriptBkdr = new ColorHash(new Options { HashFunction = new JavaScriptBkdrHash() });
var historical = new ColorHash(new Options
{
    UseJavaScriptCompatibility = false,
    HashFunction = new BKDRHash()
});
```

`JavaScriptBkdrHash` matches the JavaScript `hash: 'bkdr'` hash. Custom hashes
still use `IHashFunction`. In compatibility mode, their unsigned results
are converted to JavaScript-style double-precision numbers for color selection.
Use the historical configuration above with the same palettes and hue ranges
to reproduce historical ColorHashSharp colors. Applications upgrading from
NuGet 1.1.0 must recompile and update
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

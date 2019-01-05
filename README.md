# color-hash
Generate color based on the given string. C# port of [ColorHash Javascript Library](https://github.com/zenozeng/color-hash).


[![Build status](https://fernandezja.visualstudio.com/color-hash/_apis/build/status/color-hash-CI)](https://fernandezja.visualstudio.com/color-hash/_build/latest?definitionId=4)


#### Basic

```csharp
var colorHash = new ColorHash();

// in HSL, Hue ∈ [0, 360), Saturation ∈ [0, 1], Lightness ∈ [0, 1]
colorHash.hsl('Hello World'); // [ 225, 0.65, 0.35 ]

// in RGB, R, G, B ∈ [0, 255]
colorHash.rgb('Hello World'); // [ 135, 150, 197 ]

// in HEX
colorHash.hex('Hello World'); // '#8796c5'
```
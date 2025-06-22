# ColorHashSharp resources links

#### Links
- [Packaging Icon within the nupkg](https://github.com/NuGet/Home/wiki/Packaging-Icon-within-the-nupkg)
- [Supporting Multiple Target Frameworks](https://learn.microsoft.com/en-us/nuget/create-packages/supporting-multiple-target-frameworks)
- [NuGet Package Explorer](https://apps.microsoft.com/detail/9WZDNCRDMDM3?hl=en-us&gl=AR&ocid=pdpshare)
- [NuGet Package Explorer GitHub](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer)

#### Command to push a package to NuGet
```bash
dotnet nuget push file.nupkg -k {YOUR_API_KEY_HERE} -s https://api.nuget.org/v3/index.json
```

#### Command to pack a project
```bash
dotnet pack  --configuration Release 
```
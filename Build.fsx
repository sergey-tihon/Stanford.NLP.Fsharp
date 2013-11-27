// include Fake lib
#r @"build\FAKE\tools\FakeLib.dll"
open Fake 
open Fake.AssemblyInfoFile

// Assembly / NuGet package properties
let projectName = "FSharp.NLP.Stanford.Parser"
let version = "0.0.5"
let projectDescription = "F# wrapper for The Stanford Parser"
let authors = ["Sergey Tihon"]

// Folders
let buildDir = @".\build\bin\"
let nugetDir = @".\build\nuget\"

// Restore NuGet packages
!! "./**/packages.config"
    |> Seq.iter (RestorePackage (fun p -> 
        {p with 
            ToolPath = "./.nuget/NuGet.exe"}))

// Targets

// Update assembly info
Target "UpdateAssemblyInfo" (fun _ ->
    CreateFSharpAssemblyInfo ".\AssemblyInfo.fs"
        [ Attribute.Product projectName
          Attribute.Title projectName
          Attribute.Description projectDescription
          Attribute.Version version ]
)

// Clean build directory
Target "Clean" (fun _ ->
    CleanDir buildDir
)

// Build FSharp.NLP.Stanford.Parser
Target "BuildFSharpNLPStanford" (fun _ ->
    !! @"FSharp.NLP.Stanford.Parser\FSharp.NLP.Stanford.Parser.fsproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

// Clean NuGet directory
Target "CleanNuGet" (fun _ ->
    CleanDir nugetDir
)

// Create NuGet package
Target "CreateNuGet" (fun _ ->     
    XCopy buildDir (nugetDir @@ "lib")
    !+ @"build/nuget/lib/*.*"
        -- @"build/nuget/lib/FSharp.NLP*.*"
        |> ScanImmediately
        |> Seq.iter (System.IO.File.Delete)
        
    "FSharp.NLP.Stanford.Parser.nuspec"
      |> NuGet (fun p -> 
            {p with
                Project = projectName
                Authors = authors
                Version = version
                Description = projectDescription
                NoPackageAnalysis = true
                ToolPath = ".\.nuget\NuGet.exe"
                WorkingDir = nugetDir
                OutputPath = nugetDir })
)

// Default target
Target "Default" (fun _ ->
    trace "Building FSharp.NLP.Stanford"
)

// Dependencies
"UpdateAssemblyInfo"
  ==> "Clean"
  ==> "BuildFSharpNLPStanford"
  ==> "CleanNuGet"
  ==> "CreateNuGet"
  ==> "Default"

// start build
Run "Default"
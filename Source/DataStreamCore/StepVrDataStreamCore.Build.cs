﻿using UnrealBuildTool;
using System;
using System.IO;


public class StepVrDataStreamCore : ModuleRules
{
    private string ModulePath
    {
        get { return ModuleDirectory; }
    }
    private string LibPath
    {
        get { return Path.GetFullPath(Path.Combine(ModulePath, "../../ThirdParty")); }
    }

    public StepVrDataStreamCore(ReadOnlyTargetRules Target) : base(Target)
    {
        //OptimizeCode = CodeOptimization.InShippingBuildsOnly;
        PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

        PrivateIncludePaths.AddRange( new string[] 
        {
            "DataStreamCore/Private",
            "DataStreamCore/Public",
            "DataStreamCore/Classes",
        } );


        PrivateDependencyModuleNames.AddRange(new string[] {
            "Projects",
            "Json",
            "JsonUtilities",
        });
        PublicDependencyModuleNames.AddRange(new string[] {
            "Core",
            "CoreUObject",
            "Engine",
            "Sockets"
        });

        bool IsLibrarySupport = false;
        string LibrariesPath;
        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            IsLibrarySupport = true;

            string PlatformString = (Target.Platform == UnrealTargetPlatform.Win64) ? "x64" : "x32";
            LibrariesPath = Path.Combine(LibPath, "lib", PlatformString);

            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "StepIKClientDllCPP.lib"));
            PublicDelayLoadDLLs.Add("StepIKClientDllCPP.dll");
            RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(LibrariesPath, "StepIKClientDllCPP.dll")));
        }

        if (IsLibrarySupport)
        {
            PrivateIncludePaths.Add(Path.Combine(LibPath, "include"));
            PublicIncludePaths.Add(Path.Combine(LibPath, "include"));
        }
    }
}

# BOC-Frontend

Unity project version 2021.3.37f1 -> 2022.3.50f1

## Unity LevelPlay Plugin Integration Guide

Source: https://developers.is.com/ironsource-mobile/unity/unity-plugin/#step-1

## Issue Resolving Android Dependencies Stuck at 0% after importing UnityLevelPlay_v8.4.1.unitypackage

Source: https://www.youtube.com/watch?v=sU5njx1jn8w&t (Unity LevelPlay easy way [NEW] !! 2023)

File gradlew.bat: https://raw.githubusercontent.com/googlesamples/unity-jar-resolver/master/gradlew.bat

1. If issue happen, most likely it is because there is no gradlew.bat file in [Project Directory]\Temp\PlayServicesResolverGradle
2. Create new file named gradlew.bat, copy content from the link and paste it in the respective directory
3. Head to unity, right click on Assets folder, hit Reimport
4. If Android Dependencies didn't automatically resolve, hit Assets -> Mobile Dependency Resolver -> Android Resolver -> Resolve
5. Wait till it prompt "Resolution Succeeded."

## Issue with building app failed after installing UnityLevelPlay_v8.4.1.unitypackage
```
FAILURE: Build completed with 2 failures.

1: Task failed with an exception.
-----------
* What went wrong:
Execution failed for task ':unityLibrary:generateReleaseRFile'.
> Could not resolve all dependencies for configuration ':unityLibrary:releaseCompileClasspath'.
...
> Could not find com.ironsource.sdk:mediationsdk:8.4.0.
Required by:
    project :unityLibrary
```

Source: https://developers.is.com/ironsource-mobile/android/android-sdk/#step-1

1. Go to here and enable this: Project Settings -> Player -> Publishing Settings -> Custom Gradle Settings Template
2. This shal generate a settingTemplate.gradle under Assets/Plugins/Android/settingTemplate.gradle
3. Add these lines into settingTemplate.gradle
```
dependencyResolutionManagement {
    repositories {
        maven {
            url 'https://android-sdk.is.com/' 
        }
    }
}
```

## Issue with "CommandInvokationFailure: Failed to update Android SDK package list." Unity 2021.3 engine bug

Source: https://discussions.unity.com/t/unable-to-build-with-2021-3-34f1/937436/5

1. We are going to patch unity engine file which located at [Unity Editor Directory]\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\cmdline-tools\2.1\bin\sdkmanager.bat
2. Open the sdkManager.bat using notepad++ with administrator and change this line
```
set DEFAULT_JVM_OPTS=-Dcom.android.sdklib.toolsdir=%~dp0\..
```
to
```
set DEFAULT_JVM_OPTS="-Dcom.android.sdklib.toolsdir=%~dp0\.."
```

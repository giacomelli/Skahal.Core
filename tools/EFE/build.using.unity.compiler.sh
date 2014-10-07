UnityVersion="UNITY_4_5"
UnityBuildParameters="UNITY_IPHONE,DEBUG"

baseDir="/Users/giusepecasagrande/Git/Skahal/Skahal.Core/"

echo "<COMPILING USING UNITY COMPILER>"
compiler="/Applications/Unity/Unity.app/Contents/Frameworks/Mono/bin/mono /Applications/Unity/Unity.app/Contents/Frameworks/Mono/lib/mono/2.0/gmcs.exe"

export MONO_CFG_DIR=/Applications/Unity/Unity.app/Contents/Frameworks/Mono/etc
export MONO_PATH=/Applications/Unity/Unity.app/Contents/Frameworks/Mono/lib/mono/2.0

echo SKAHAL.INFRASTRUCTURE.FRAMEWORK
sourceFiles=`find $baseDir/src/Skahal.Infrastructure.Framework -type f -name '*.cs'`
$compiler -debug -target:library -nowarn:0169 -out:'Skahal.Infrastructure.Framework.dll' -define:"$UnityVersion" -define:"$UnityBuildParameters" -r:HelperSharp.dll -r:System.IO.Abstractions.dll -doc:Skahal.Infrastructure.Framework.xml $sourceFiles

echo SKAHAL.DOMAIN
sourceFiles=`find $baseDir/src/Skahal.Domain -type f -name '*.cs'`
$compiler -debug -target:library -nowarn:0169 -out:'Skahal.Domain.dll' -define:$UnityVersion -define:$UnityBuildParameters -r:Skahal.Infrastructure.Framework.dll -doc:Skahal.Domain.xml $sourceFiles

echo SKAHAL.INFRASTRUCTURE.UNITY.EXTERNALS
sourceFiles=`find $baseDir/src/Skahal.Infrastructure.Unity.Externals -type f -name '*.cs'`
$compiler -debug -target:library -nowarn:0169 -out:'Skahal.Infrastructure.Unity.Externals.dll' -define:$UnityVersion -define:$UnityBuildParameters -r:"$baseDir/src/references/UnityEngine.dll" -r:"$baseDir/src/references/Photon3Unity3D.dll" $sourceFiles

echo SKAHAL.INFRASTRUCTURE.UNITY
sourceFiles=`find $baseDir/src/Skahal.Infrastructure.Unity -type f -name '*.cs'`
$compiler -debug -target:library -nowarn:0169 -out:'Skahal.Infrastructure.Unity.dll' -define:$UnityVersion -define:$UnityBuildParameters -r:Skahal.Domain.dll -r:Skahal.Infrastructure.Framework.dll -r:Skahal.Infrastructure.Unity.Externals -r:Skahal.Infrastructure.Framework.ProtobufSerializer -r:"$baseDir/src/references/UnityEngine.dll" -r:"$baseDir/src/References/protobuf-net/CoreOnly/ios/Protobuf-net.dll" -r:"$baseDir/src/References/Photon3Unity3D.dll" $sourceFiles

echo "</COMPILING USING UNITY COMPILER>"

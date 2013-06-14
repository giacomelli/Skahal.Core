echo "<COMPILING USING UNITY COMPILER>"

export MONO_CFG_DIR=/Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/etc
export MONO_PATH=/Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/lib/mono/2.0

echo SKAHAL.INFRASTRUCTURE.FRAMEWORK
sourceFiles=`find /Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/Skahal.Infrastructure.Framework -type f -name '*.cs'`
/Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/bin/mono /Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/lib/mono/2.0/gmcs.exe -debug -target:library -nowarn:0169 -out:'Skahal.Infrastructure.Framework.dll' -define:UNITY_3_5_7 -define:UNITY_IPHONE,DEBUG -r:HelperSharp.dll -doc:Skahal.Infrastructure.Framework.xml $sourceFiles

echo SKAHAL.DOMAIN
sourceFiles=`find /Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/Skahal.Domain -type f -name '*.cs'`
/Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/bin/mono /Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/lib/mono/2.0/gmcs.exe -debug -target:library -nowarn:0169 -out:'Skahal.Domain.dll' -define:UNITY_3_5_7 -define:UNITY_IPHONE -r:Skahal.Infrastructure.Framework.dll -doc:Skahal.Domain.xml $sourceFiles

echo SKAHAL.INFRASTRUCTURE.UNITY.EXTERNALS
sourceFiles=`find /Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/Skahal.Infrastructure.Unity.Externals -type f -name '*.cs'`
/Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/bin/mono /Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/lib/mono/2.0/gmcs.exe -debug -target:library -nowarn:0169 -out:'Skahal.Infrastructure.Unity.Externals.dll' -define:UNITY_3_5 -define:UNITY_IPHONE -r:/Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/references/UnityEngine.dll -r:/Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/references/Photon3Unity3D.dll $sourceFiles

echo SKAHAL.INFRASTRUCTURE.UNITY
sourceFiles=`find /Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/Skahal.Infrastructure.Unity -type f -name '*.cs'`
/Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/bin/mono /Applications/Unity_3.5.7f6/Unity.app/Contents/Frameworks/Mono/lib/mono/2.0/gmcs.exe -debug -target:library -nowarn:0169 -out:'Skahal.Infrastructure.Unity.dll' -define:UNITY_3_5_7 -define:UNITY_IPHONE -r:Skahal.Domain.dll -r:Skahal.Infrastructure.Framework.dll -r:Skahal.Infrastructure.Unity.Externals -r:Skahal.Infrastructure.Framework.ProtobufSerializer -r:/Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/references/UnityEngine.dll -r:/Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/References/protobuf-net/CoreOnly/ios/Protobuf-net.dll -r:/Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/references/Photon3Unity3D.dll $sourceFiles

echo "</COMPILING USING UNITY COMPILER>"
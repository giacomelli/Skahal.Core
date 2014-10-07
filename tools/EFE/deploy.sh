projectName="EFE"
baseDir="/Users/giusepecasagrande/Git/Skahal/Skahal.Core"
baseDestDir="/Users/giusepecasagrande/Git/Escape from Eden"

dest="$baseDestDir/src/Escape from Eden/Assets/_Assets/Scripts/Libs"


cd "$baseDir/src/build"
/bin/bash "$baseDir/tools/EFE/build.using.unity.compiler.sh"

echo "<COPYING ASSEMBLIES TO $projectName>"

cp -f Skahal.Domain.dll "$dest"
cp -f Skahal.Infrastructure.Framework.dll "$dest"
cp -f Skahal.Infrastructure.Unity.dll "$dest"
cp -f Skahal.Infrastructure.Unity.Externals.dll "$dest"
cp -f Skahal.Infrastructure.Framework.ProtobufSerializer.dll "$dest"
cp -f HelperSharp.dll "$dest"

cp -f Skahal*.xml "$dest"
cp -f protobuf*.dll "$dest"

echo "</COPYING ASSEMBLIES TO $projectName>"

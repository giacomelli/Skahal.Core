cd /Users/giacomelli/Dropbox/Skahal/middleware/Skahal.Core/src/build

echo "<COPYING ASSEMBLIES TO jogosdaqui>"
dest="/Users/giacomelli/Dropbox/jogosdaqui/Plataforma/src/references"
cp Skahal.Domain.dll $dest
cp Skahal.Domain.dll.mdb $dest
cp Skahal.Domain.xml $dest
cp Skahal.Infrastructure.Framework.dll $dest
cp Skahal.Infrastructure.Framework.dll.mdb $dest
cp Skahal.Infrastructure.Framework.xml $dest
echo "</COPYING ASSEMBLIES TO jogosdaqui>"

./build.using.unity.compiler.sh

echo "<COPYING ASSEMBLIES TO HATCG>"
dest="/Users/giacomelli/Dropbox/Skahal/games/HATCG/dev/src/HATCG/Assets/_Assets/Libs"

cp Skahal.Domain.dll $dest
cp Skahal.Infrastructure.Framework.dll $dest
cp Skahal.Infrastructure.Unity.dll $dest
cp Skahal.Infrastructure.Unity.Externals.dll $dest
cp Skahal.Infrastructure.Framework.ProtobufSerializer.dll $dest
cp HelperSharp.dll $dest

cp Skahal*.xml $dest
cp Photon*.dll $dest
cp protobuf*.dll $dest

echo "</COPYING ASSEMBLIES TO HATCG>"
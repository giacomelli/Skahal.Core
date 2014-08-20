cd /Users/giusepecasagrande/Git/Skahal.Core/src/build
# 
# echo "<COPYING ASSEMBLIES TO jogosdaqui>"
# dest="/Users/giacomelli/Dropbox/jogosdaqui/Plataforma/src/references"
# cp Skahal.Domain.dll $dest
# cp Skahal.Domain.dll.mdb $dest
# cp Skahal.Domain.xml $dest
# cp Skahal.Infrastructure.Framework.dll $dest
# cp Skahal.Infrastructure.Framework.dll.mdb $dest
# cp Skahal.Infrastructure.Framework.xml $dest
# echo "</COPYING ASSEMBLIES TO jogosdaqui>"

/Users/giusepecasagrande/Git/Skahal.Core/tools/build.using.unity.compiler.sh

echo "<COPYING ASSEMBLIES TO HATCG>"
dest="/Users/giusepecasagrande/Git/HATCG/src/HATCG/Assets/_Assets/Libs"

cp -f Skahal.Domain.dll $dest
cp -f Skahal.Infrastructure.Framework.dll $dest
cp -f Skahal.Infrastructure.Unity.dll $dest
cp -f Skahal.Infrastructure.Unity.Externals.dll $dest
cp -f Skahal.Infrastructure.Framework.ProtobufSerializer.dll $dest
cp -f HelperSharp.dll $dest

cp -f Skahal*.xml $dest
# cp -f Photon*.dll $dest
cp -f protobuf*.dll $dest

echo "</COPYING ASSEMBLIES TO HATCG>"
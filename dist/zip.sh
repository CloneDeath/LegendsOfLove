NAME=legends_of_love
pushd html5
zip -r $NAME-html5.zip ./*
popd

pushd windows
zip -r $NAME-windows.zip ./*
popd

pushd linux
zip -r $NAME-linux.zip ./*
popd

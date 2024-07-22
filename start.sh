echo "==============(Re)Build Image=============="

if command -v docker &> /dev/null; then

    echo "Docker is installed"

    echo "==============Stop Container (if exists)============"
    docker rm chuk-norris-jokes-service

    docker build -t onadebi/chuck-norris-jokes .

    echo "==============Run Container=============="
    docker run --name chuk-norris-jokes-service -d -p 5000:80 onadebi/chuck-norris-jokes
else
    echo "Docker is not installed"
    exit 1
fi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY "NswLinkExtractor.csproj" ./
RUN dotnet restore

COPY . ./
RUN dotnet build "NswLinkExtractor.csproj" --configuration Release --output /app/build

RUN dotnet publish "NswLinkExtractor.csproj" --configuration Release --output /app/publish --self-contained true -r linux-x64

FROM public.ecr.aws/lambda/dotnet:8

WORKDIR /var/task


# Install dependencies for Selenium and Chrome
#RUN yum install -y \
    #wget \
    #unzip \
    #fontconfig \
    #&& wget https://dl.google.com/linux/direct/google-chrome-stable_current_x86_64.rpm \
    #&& yum install -y ./google-chrome-stable_current_x86_64.rpm \
    #&& rm google-chrome-stable_current_x86_64.rpm \
    #&& wget https://chromedriver.storage.googleapis.com/$(curl -sS chromedriver.storage.googleapis.com/LATEST_RELEASE)/chromedriver_linux64.zip \
    #&& unzip chromedriver_linux64.zip \
    #&& rm chromedriver_linux64.zip \
    #&& mv chromedriver /usr/local/bin/chromedriver \
    #&& yum clean all
#
## Set environment variable for Chrome
#ENV PATH="/usr/local/bin:$PATH"

FROM selenium/standalone-chrome
WORKDIR /app
COPY --from=build /app/publish .

CMD [ "NswLinkExtractor::NswLinkExtractor.Function::FunctionHandler" ]

# This COPY command copies the .NET Lambda project's build artifacts from the host machine into the image. 
# The source of the COPY should match where the .NET Lambda project publishes its build artifacts. If the Lambda function is being built 
# with the AWS .NET Lambda Tooling, the `--docker-host-build-output-dir` switch controls where the .NET Lambda project
# will be built. The .NET Lambda project templates default to having `--docker-host-build-output-dir`
# set in the aws-lambda-tools-defaults.json file to "bin/Release/lambda-publish".
#
# Alternatively Docker multi-stage build could be used to build the .NET Lambda project inside the image.
# For more information on this approach checkout the project's README.md file.

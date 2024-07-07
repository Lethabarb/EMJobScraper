FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 35491

#RUN apt-get update; apt-get clean
#RUN apt-get install -y wget
#RUN apt-get install -y gnupg
#RUN apt-get update && apt-get -y install libglib2.0-0 libxi6 libnss3
#RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
    #&& echo "deb https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb"
#RUN apt-get update && apt-get -y install google-chrome-stable
RUN apt-get update -qq -y && \
    apt-get install -y \
        libasound2 \
        libatk-bridge2.0-0 \
        libgtk-4-1 \
        libnss3 \
        xdg-utils \
        wget && \
    wget -q -O chrome-linux64.zip https://storage.googleapis.com/chrome-for-testing-public/126.0.6478.126/linux64/chrome-linux64.zip && \
    unzip chrome-linux64.zip && \
    rm chrome-linux64.zip && \
    mv chrome-linux64 /opt/chrome/ && \
    ln -s /opt/chrome/chrome /usr/local/bin/ && \
    wget -q -O chromedriver-linux64.zip https://storage.googleapis.com/chrome-for-testing-public/126.0.6478.126/linux64/chromedriver-linux64.zip && \
    unzip -j chromedriver-linux64.zip chromedriver-linux64/chromedriver && \
    rm chromedriver-linux64.zip && \
    mv chromedriver /usr/local/bin/

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY "NswLinkExtractor.csproj" ./
RUN dotnet restore

COPY . ./
RUN dotnet build "NswLinkExtractor.csproj" --configuration Release --output /app/build

RUN dotnet publish "NswLinkExtractor.csproj" --configuration Release --output /app/publish --self-contained true -r linux-x64


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

FROM public.ecr.aws/lambda/dotnet:8
WORKDIR /var/task
COPY --from=build /app/publish .

CMD ["NswLinkExtractor::NswLinkExtractor.Function::FunctionHandler" ]

# This COPY command copies the .NET Lambda project's build artifacts from the host machine into the image. 
# The source of the COPY should match where the .NET Lambda project publishes its build artifacts. If the Lambda function is being built 
# with the AWS .NET Lambda Tooling, the `--docker-host-build-output-dir` switch controls where the .NET Lambda project
# will be built. The .NET Lambda project templates default to having `--docker-host-build-output-dir`
# set in the aws-lambda-tools-defaults.json file to "bin/Release/lambda-publish".
#
# Alternatively Docker multi-stage build could be used to build the .NET Lambda project inside the image.
# For more information on this approach checkout the project's README.md file.

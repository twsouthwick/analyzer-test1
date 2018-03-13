####################################################
# Docker image for performance testing of .NET Core.
####################################################
FROM microsoft/dotnet:2.0.5-sdk-2.1.4-stretch

# Install curl so that we can download dependencies.
RUN apt-get -y update && apt-get install -y curl

# Install CLI dependencies.
#RUN apt-get -y install libunwind8 gettext libicu52 libuuid1 libcurl3 libssl1.0.0 zlib1g liblttng-ust0
# Create, restore and build a new HelloWorld application.
RUN mkdir hw && cd hw && dotnet new console && \
	echo "using System;\n\nnamespace ConsoleApplication\n{\n\tpublic class Program\n\t{\n\t\tpublic static void Main(string[] args)\n\t\t{\n\t\t\tConsole.WriteLine(\"This application will allocate new objects in a loop forever.\");\n\t\t\twhile(true){ object o = new object(); }\n\t\t}\n\t}\n}" > Program.cs && \
	dotnet restore && dotnet build -c Release

# Download the latest perfcollect.
RUN mkdir /perf && cd /perf && curl -OL https://aka.ms/perfcollect && chmod +x perfcollect

RUN apt-get -y install zip liblttng-ust-dev lttng-tools linux-tools
# Install perf and LTTng dependencies.

# Set tracing environment variables.
ENV COMPlus_PerfMapEnabled 1
ENV COMPlus_EnableEventLog 1

# Run the app.
CMD cd /hw && dotnet run -c Release
####################################################
# Docker image for performance testing of .NET Core.
####################################################
FROM tasou/perf

# Install curl so that we can download dependencies.
RUN apt-get -y update && apt-get install -y curl

# Download the latest perfcollect.
RUN mkdir /perf && cd /perf && curl -OL https://aka.ms/perfcollect && chmod +x perfcollect

RUN apt-get -y install zip liblttng-ust-dev lttng-tools linux-tools
# Install perf and LTTng dependencies.

RUN echo 'alias perf="/usr/bin/perf_3.16"' >> ~/.bashrc
# Set tracing environment variables.
ENV COMPlus_PerfMapEnabled 1
ENV COMPlus_EnableEventLog 1

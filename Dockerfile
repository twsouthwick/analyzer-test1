####################################################
# Docker image for performance testing of .NET Core.
####################################################
FROM tasou/perf

# Install curl so that we can download dependencies.
RUN apt-get -y update && apt-get install -y curl

# Download the latest perfcollect.
RUN mkdir /perf && cd /perf && curl -OL https://aka.ms/perfcollect && chmod +x perfcollect

RUN apt-get -y install zip liblttng-ust-dev lttng-tools linux-tools lttng-modules-dkms

RUN apt-get -y install vim

# Install perf and LTTng dependencies.
RUN cp /usr/bin/perf_3.16 /usr/bin/perf

# This seems to initialize something so you don't see warnings later
RUN lttng --version
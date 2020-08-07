FROM ubuntu:20.04
RUN apt-get update
RUN apt-get install -y ruby \
    bundler \
    ri \
    ruby-dev
RUN gem install thrift 
COPY ./StreamThrift ./streamingServer/StreamThrift
COPY ./Media ./streamingServer/Media
WORKDIR /streamingServer/StreamThrift
EXPOSE 8000
CMD ruby StreamingServer.rb
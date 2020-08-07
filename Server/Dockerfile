FROM ubuntu:20.04
RUN apt-get upgrade -y
RUN apt-get update && apt-get -y install --no-install-recommends \ 
    gnupg2 \
    curl \
    python3.8 \
    python3-pip \
    python3-venv \
    python3-dev \
    libpq-dev \
    unixodbc-dev \
    g++
RUN python3 -m pip install \ 
    pyodbc
RUN curl https://packages.microsoft.com/config/ubuntu/19.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
RUN apt-key adv --keyserver keyserver.ubuntu.com --recv-keys EB3E94ADBE1229CF
RUN apt-get update && ACCEPT_EULA=Y apt-get -y install --no-install-recommends \  
    msodbcsql17 \
    mssql-tools
RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
RUN apt-get -y install python3-thriftpy
RUN python3 -m pip install thrift
COPY /Server ./spotifake/Server
COPY /SQLConnection ./spotifake/SQLConnection
COPY /Media ./spotifake/Media
COPY /configuration.py ./spotifake/configuration.py
COPY /Thrift ./spotifake/Thrift
WORKDIR /spotifake/Server
EXPOSE 5000
CMD python3 SpotifakeServer.py

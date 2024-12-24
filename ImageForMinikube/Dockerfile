FROM bitnami/dotnet:8.0.11-debian-12-r2
WORKDIR /app
COPY bin/Debug/ImageForMinikube.1.0.0.nupkg .
COPY bin/Debug/net8.0 /app/Debug/net8.0
COPY obj /app/obj
ENTRYPOINT ["dotnet","/app/Debug/net8.0/ImageForMinikube.dll"]
ENV DB_HOST=172.24.0.11
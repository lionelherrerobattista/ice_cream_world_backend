# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remainig source code and build
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/out .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Start the app
ENTRYPOINT [ "dotnet", "ice_cream_world_backend.dll" ]

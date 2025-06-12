# First stage base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files
COPY *.csproj ./
COPY ./ ./ 

# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o /app

# Final stage
# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app .

# Expose the port the application runs on
EXPOSE 80
EXPOSE 443

# Start the application
ENTRYPOINT ["dotnet", "smartMonitorApp.dll"]
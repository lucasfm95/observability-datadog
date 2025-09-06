FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ARG datadog_agent_installpackage_version=3.25.0
RUN apt-get update && apt-get install -y --no-install-recommends curl
RUN curl -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v3.25.0/datadog-dotnet-apm_3.25.0_arm64.deb \
&& dpkg -i ./datadog-dotnet-apm_3.25.0_arm64.deb

ENV ASPNETCORE_ENVIRONMENT=production
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80

ENTRYPOINT [ "dotnet", "/app/SampleApi.dll" ]
ARG DOTNET_VERSION=3.1
ARG BASE_BUILD_IMAGE=base-build-env

# Stage 1 - build base environment, might be skipped if BASE_BUILD_IMAGE is passed
FROM mcr.microsoft.com/dotnet/core/sdk:${DOTNET_VERSION}-alpine AS base-build-env
ENV NODE_VERSION=12
ENV FORCE_COLOR=true
ENV NODE_PATH "/usr/local/lib/node_modules"
RUN apk update -q --no-progress && \
  apk add -q --no-progress --no-cache git g++ make python3 npm nodejs=~${NODE_VERSION}
WORKDIR /src/MedicalAssistant.SurveyCovid.App/ClientApp 
COPY ./MedicalAssistant.SurveyCovid.App/ClientApp/package.json ./MedicalAssistant.SurveyCovid.App/ClientApp/package-lock.json ./
RUN npm ci
WORKDIR /src
COPY ./*.sln ./*.props ./*/*.csproj ./ 
COPY . ./

# Stage 2 - build app and publish
FROM ${BASE_BUILD_IMAGE} AS build-env

WORKDIR /src/MedicalAssistant.SurveyCovid.App
RUN dotnet build -c Release && \
  dotnet publish -c Release -o ../output --no-build /p:TreatWarningsAsErrors=true

# Stage 3 - run the app
FROM mcr.microsoft.com/dotnet/core/aspnet:${DOTNET_VERSION}-alpine
WORKDIR /app
COPY --from=build-env /src/output ./
# ENTRYPOINT [ "dotnet", "MedicalAssistant.SurveyCovid.App.dll" ]

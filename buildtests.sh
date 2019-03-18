#!/bin/sh
dotnet restore src/KhufuServer
dotnet build src/KhufuServer

dotnet restore tests/KhufuServer.Tests
dotnet build tests/KhufuServer.Tests
dotnet test tests/KhufuServer.Tests

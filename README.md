# csharp-pokedex
C# fun Pokedex in the form of REST API

[![CI](https://github.com/lucazulian/csharp-pokedex/actions/workflows/dotnetcore.yml/badge.svg)](https://github.com/lucazulian/csharp-pokedex/actions/workflows/dotnetcore.yml)

## Requirements
  
  - docker **20+**
  - GNU make **4+**
  - dotnet **5.0.206**

## Repository conventions

  * [Conventional Commits][1]

  [1]: https://www.conventionalcommits.org/en/v1.0.0/

## Getting started

#### make commands

```bash
help                           Get help information about make available commands

build                          Build Docker container application
run                            Run Docker container application
```

## How to use
TODO

## Improvements / Missing parts
- [ ] add distributed cache in order to avoid unnecessary requests and to speed up hypothetical workload;
- [ ] add tracing system for complete application observability;
- [ ] add rate limiting in order to avoid excessive 3rd part systems workload;

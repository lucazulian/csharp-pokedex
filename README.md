# csharp-pokedex
C# fun Pokedex in the form of REST API

[![CI](https://github.com/lucazulian/csharp-pokedex/actions/workflows/dotnetcore.yml/badge.svg)](https://github.com/lucazulian/csharp-pokedex/actions/workflows/dotnetcore.yml)

## Requirements
  
  - docker **20+**
  - GNU make **4+**

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

The application is containerized in Docker and uses makefile as utility.
Check basic requirements listed in `Requirements` section.

To run the application follow these steps:

Open a terminal and start the application with this command:
```bash
  make run
```
the container with the API should now be running on port 5000 and it should start logging some information.

For the first endpoint `Basic Pokemon Information` run this command:

```bash
  http http://localhost:5000/pokemon/bulbasaur
```

For the second endpoint `Translated Pokemon Description` run this command:
```bash
  http http://localhost:5000/pokemon/translated/mewtwo
```

## Improvements / Missing parts
- [ ] add distributed cache in order to avoid unnecessary requests and to speed up hypothetical workload;
- [ ] add tracing system for complete application observability;
- [ ] add rate limiting in order to avoid excessive 3rd part systems workload;
- [ ] in test suite avoid real request to 3rd part API;

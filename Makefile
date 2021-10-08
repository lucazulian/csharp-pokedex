##
# Make utility to automate boring and repetitive commands
#
# @file Makefile
# @version 0.1

all: build
.PHONY: build run

build:			## Build Docker container application
	docker build -t pokedex .

run: build		## Run Docker container application
	docker run -p 5000:80 -e "ASPNETCORE_ENVIRONMENT=Development" -t pokedex

help:
	@echo "Usage: make [command]"
	@echo "Make utility to automate boring and repetitive commands."
	@echo
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'
	@echo

# end

#!/bin/bash

DEFAULT_PREFIX="ma"

function sleep_and_exit(){
  sleep 1;
  exit 1;
}

function echo_error(){
  echo "$(tput setaf 1)Error: $1$(tput sgr0)";
}

function echo_warning(){
  echo "$(tput setaf 3)Warning: $1$(tput sgr0)";
}

function set_default_prefix(){
  if [[ -z "${PREFIX}" ]]; then
    echo_warning "Setting default compose prefix: $DEFAULT_PREFIX";
    echo "To override compose prefix, set environment variable \"PREFIX\"";
    export PREFIX=$DEFAULT_PREFIX
  fi
}

set_default_prefix

environment_name=$1
shift
command=$@


case "$environment_name" in
  dev)
    environment_str="-f .docker/dev.docker-compose.yml"
    ;;
  ci)
    environment_str="-f .docker/ci.docker-compose.yml"
    ;;
  qa)
    environment_str="-f .docker/qa.docker-compose.yml"
    ;;
  *)
    echo "Unknown environment: $environment_name"
    sleep_and_exit
esac

eval "docker-compose $environment_str -p $PREFIX $command"

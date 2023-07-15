# Doggo
> API to find person for a walk with a dog

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Screenshots](#screenshots)
* [Setup](#setup)
* [Project Status](#project-status)
* [Room for Improvement](#room-for-improvement)


## General Information
- This is PET project that solve problem to find someone who will walk with your dog, the main purpose to create this project is to show my coding skills to employer.


## Technologies Used
- MediatR - for implementation of business logic
- PostgreSQL - as main DataBase
- EFCore.NamingConventions - for use SnaceCase in postgres
- Redis - as cache service
- Serilog - as logger
- Seq - for structure log data
- FluentValidation - for validate requests
- SignalR - for real-time chat
- JwtBearer, Google.Apis.Auth - for authentication
- AWS S3 - for storing images
- XUnit, Moq, FluentAssertions - for Unit tests
- MailKit - for mail service
- AspNetCore.HealthChecks - for health checs
- Polly - fro handlig failed httpRequest to third party API
- 


## Features
- Authentication with Google, Facebook
- Storing images in S3 bucket
- Real-time chat


## Screenshots
![Example screenshot](./img/screenshot.png)
<!-- If you have screenshots you'd like to share, include them here. -->


## Setup


## Project Status
Project is: _in progress_ 


## Room for Improvement

Room for improvement:
- Change endpoints routes to some naming convention
- Complete unit tests
- 

To do:
- Add payment system
- Add images compression
- Add integration tests

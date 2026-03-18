# MineralVault

[![CI](https://github.com/Pbeier-testing/MineralVault/actions/workflows/pipeline.yml/badge.svg)](https://github.com/Pbeier-testing/MineralVault/actions)
![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)

A full-stack application for managing a personal mineral collection, including location tracking and image support.

This project is part of my portfolio and focuses on combining **software development** with **structured testing practices**.

---

## Features

* Manage mineral entries
* Store and display images
* Track find locations (map integration planned)

---

## Architecture

The project follows a simplified **Clean Architecture** approach:

* `Domain` – Core entities and business logic
* `Application` – Use cases and interfaces *(planned)*
* `API` – ASP.NET Core Web API
* `Infrastructure` – Database and external services *(planned)*

This structure keeps the code modular, testable, and easy to extend.

---

## Tech Stack

**Backend**

* ASP.NET Core Web API (.NET 10)
* Entity Framework Core
* SQLite
* REST API with Swagger UI for testing endpoints
* Automated testing and CI pipeline

**Frontend**

* Blazor WebAssembly *(in progress)*

**Testing**

* xUnit (unit tests)
* Playwright (E2E – planned)

**CI/CD**

* GitHub Actions

---

## Testing

With a background in software testing (ISTQB Foundation Level), I place a strong focus on testability and automated testing.

* Unit tests for business logic
* Integration tests for API and database *(planned)*
* End-to-end tests using Playwright *(planned)*
* Automated test execution via CI pipeline

**Detailed testing documentation:**
See [`/docs/testing`](docs/testing)

---

## Getting Started

### Prerequisites

* .NET 10 SDK
* EF Core Tools

  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Setup

1. Clone the repository

   ```bash
   git clone https://github.com/Pbeier-testing/MineralVault.git
   cd MineralVault
   ```

2. Apply database migrations

   ```bash
   dotnet ef database update --project src/MineralCollection.API
   ```

3. Run the API

   ```bash
   dotnet run --project src/MineralCollection.API
   ```

4. Open Swagger UI
   
   ```
   http://localhost:<port>/swagger
   ```
   Open Swagger UI using the URL shown in the console output after starting the application.

---

## Roadmap

* [x] Initial project setup
* [x] SQLite integration
* [x] Basic CI pipeline
* [ ] Application layer (use cases)
* [ ] Frontend integration (Blazor)
* [ ] Map visualization (Leaflet)
* [ ] Integration tests
* [ ] E2E tests with Playwright

---

## Purpose

This project demonstrates:

* Full-stack development with .NET
* Clean and maintainable architecture
* Integration of testing into the development process
* Use of CI/CD for automated quality assurance

---

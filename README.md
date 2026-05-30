# SaaS Billing System

A multi-tenant SaaS Billing System built with ASP.NET Core.  
This project demonstrates clean architecture, authentication, organization-based tenancy, and subscription management.

---

## 🚀 Features

- User Registration & Login (JWT Authentication)
- Multi-tenant Organization support
- Organization uniqueness validation
- Password hashing
- Role-based authorization (future/extendable)
- Caching layer integration
- Clean Architecture (API, Application, Domain, Infrastructure)
- EF Core with Migrations
- Docker support (if added)

---

## 🏗️ Architecture

This project follows Clean Architecture:

- **API Layer** → Controllers / Endpoints
- **Application Layer** → Business logic (MediatR)
- **Domain Layer** → Entities & core rules
- **Infrastructure Layer** → Database, repositories, external services

---

## 🛠️ Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- MediatR
- FluentValidation (optional)
- SQL Server
- Docker (optional)
- Redis Cache (if used)

---

## ⚙️ Getting Started

### 1. Clone repository

```bash
git remote add origin https://github.com/MuhammadUmar-dotnetdev/SaaSBillingSystem.git

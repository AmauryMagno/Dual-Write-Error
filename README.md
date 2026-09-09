# 🔄 Dual-Write-Error

> A hands-on study about the **Dual Write problem**, its failure scenarios, and how the **Outbox Pattern** can be used to build more reliable APIs.

This repository was created to **understand, reproduce, and solve the Dual Write problem** in a practical way.

The goal is not only to show the final solution, but to make the entire journey visible:

**Problem → Experiment → Failure → Investigation → Solution 🚀**

---

## 🎯 About the Project

The **Dual Write problem** happens when an API needs to update multiple data sources as part of a single operation.

For example:

```text
API Request
    │
    ├──► Database
    │
    └──► Message Broker
```

At first glance, this looks simple.

But what happens if the database operation succeeds and the message broker fails?

```text
API Request
    │
    ├── ✅ Database updated
    │
    └── ❌ Message failed
```

Now the systems are **out of sync**.

This repository explores this scenario and demonstrates how the **Outbox Pattern** can help solve it.

---

## 🧪 What You'll Find Here

The project is organized as a practical evolution of the problem.

### 1️⃣ Basic API

A simple API that provides the foundation for the experiments.

### 2️⃣ Dual Write

An implementation that performs multiple writes during the same operation.

### 3️⃣ Dual Write Failure

Experiments designed to reproduce failure scenarios and demonstrate how data inconsistency can occur.

### 4️⃣ Outbox Pattern

A solution that uses the **Outbox Pattern** to reliably persist events/messages before they are processed asynchronously.

---

## 🌿 Branch Structure

Each branch represents a different stage of the study:

```text
main
 │
 └── develop
      │
      ├── feature/basic-api
      │
      ├── experiment/dual-write
      │
      ├── experiment/dual-write-failure
      │
      └── solution/outbox-pattern
```

### 📌 Branches

| Branch | Description |
|---|---|
| `feature/basic-api` | Basic API implementation |
| `experiment/dual-write` | Demonstrates the Dual Write approach |
| `experiment/dual-write-failure` | Reproduces failure and inconsistency scenarios |
| `solution/outbox-pattern` | Implements the Outbox Pattern solution |

The idea is to be able to **switch between branches and inspect each stage independently**.

---

## 🏗️ Project Architecture

The solution follows a layered architecture, separating responsibilities between API, application logic, domain and persistence.

```text
                    ┌──────────────────────┐
                    │   DualWrite.RFE.Api  │
                    │       🌐 API         │
                    └──────────┬───────────┘
                               │
                               ▼
              ┌──────────────────────────────┐
              │   DualWrite.RFE.Application  │
              │       ⚙️ Use Cases           │
              └──────────────┬───────────────┘
                             │
                             ▼
                    ┌──────────────────────┐
                    │  DualWrite.RFE.Domain│
                    │       🧠 Domain       │
                    └──────────────────────┘

              ┌────────────────────────────┐
              │   DualWrite.RFE.Database   │
              │       💾 Persistence       │
              └─────────────┬──────────────┘
                            │
                            ▼
                    ┌──────────────────────┐
                    │  DualWrite.RFE.Domain│
                    │       🧠 Domain       │
                    └──────────────────────┘
```

### Responsibilities

**🌐 `DualWrite.RFE.Api`**

Responsible for HTTP communication, controllers, dependency injection and API configuration.

**⚙️ `DualWrite.RFE.Application`**

Contains application use cases, services and orchestration logic.

**🧠 `DualWrite.RFE.Domain`**

Contains the core business rules, entities, value objects and domain abstractions.

**💾 `DualWrite.RFE.Database`**

Responsible for persistence, database context, repositories and data access implementations.

---

## 🛠️ Tech Stack

| Technology | Usage |
|---|---|
| 🟣 **C#** | Main programming language |
| 🟦 **.NET** | Application framework |
| 🗄️ **Database** | Data persistence |
| 🐳 **Docker** | Infrastructure/environment |
| 📡 **REST API** | Communication |
| 📦 **Outbox Pattern** | Reliable event/message processing |

---

## 🔥 The Problem

A simplified Dual Write operation might look like this:

```text
        ┌─────────────┐
        │    Client   │
        └──────┬──────┘
               │
               ▼
        ┌─────────────┐
        │     API     │
        └──────┬──────┘
               │
          ┌────┴────┐
          ▼         ▼
     ┌────────┐  ┌─────────────┐
     │   DB   │  │ Message     │
     │   ✅   │  │ Broker ❌   │
     └────────┘  └─────────────┘
```

The problem is that these operations are **not automatically atomic across different systems**.

One operation can succeed while another fails.

Result:

```text
❌ Inconsistent state
❌ Lost messages
❌ Difficult recovery
❌ Data synchronization problems
```

---

## 💡 The Solution

The **Outbox Pattern** changes the flow.

Instead of directly writing to the database and message broker:

```text
API
 │
 ├──► Database
 │
 └──► Message Broker
```

we first persist the event in an **Outbox** as part of the same database transaction:

```text
API
 │
 ▼
┌─────────────────────────┐
│       Database          │
│                         │
│  Business Data    ✅    │
│  Outbox Event     ✅    │
└────────────┬────────────┘
             │
             ▼
       Background Worker
             │
             ▼
       Message Broker
             │
             ✅
```

This allows the application to reliably retry message processing without losing the event when the external system is temporarily unavailable.

---

## 📚 Learning Goals

This project aims to explore:

- 🔄 Dual Write problems
- 💥 Distributed failure scenarios
- 🔐 Data consistency
- 🧩 Separation of concerns
- 🏗️ Layered architecture
- 📦 Outbox Pattern
- 🔁 Retry and message processing
- ⚡ Asynchronous processing
- 🧪 Failure simulation
- 🛠️ Reliable API design

---

## 🚀 Getting Started

Clone the repository:

```bash
git clone <repository-url>
```

Navigate to the project:

```bash
cd Dual-Write-Error
```

Navigate to the API:

```bash
cd RFE-Api
```

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run the API:

```bash
dotnet run
```

Swagger will be available at:

```text
http://localhost:5059/swagger
```

---

## 🗺️ Project Roadmap

```text
[✅] Basic API
     │
     ▼
[🚧] Dual Write implementation
     │
     ▼
[🚧] Failure simulation
     │
     ▼
[📋] Failure analysis
     │
     ▼
[📋] Outbox implementation
     │
     ▼
[📋] Background processing
     │
     ▼
[📋] Retry mechanism
     │
     ▼
[🎯] Reliable event processing
```

---

## 📖 Why This Project?

Distributed systems often fail in ways that are difficult to reproduce and understand.

The purpose of this repository is to make one of these problems **visible and reproducible**, showing not only **how the problem happens**, but also **why it happens and how to design a more reliable solution**.

> **Don't just implement the solution. Understand the problem first.** 🧠

---

## 👨‍💻 Author

Developed by Amaury Magno as a study project focused on **.NET, distributed systems, data consistency and reliable messaging patterns**.

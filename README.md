# Embedded Integration Backend

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13.0-239120?style=for-the-badge&logo=csharp&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![OPC UA](https://img.shields.io/badge/OPC%20UA-Industrial-FF6600?style=for-the-badge)

**High-performance backend server for real-time industrial automation integration**

[Technologies](#🛠️-technologies) • [Getting Started](#🚀-getting-started)

</div>

---

## Overview

The **Embedded Integration Backend** is the **central orchestration layer** of the system. Built with **.NET 10** and following **Clean Architecture principles**, it exposes REST APIs and a **SignalR Hub** to manage, cache, and broadcast industrial data in real time.

It acts as the **single source of truth** between:

- Industrial data received from the **Bridge**
- Real-time UI consumers (**Front-end**)
- Simulation and command workflows

---

### 🏭 System Architecture - Industrial Integration

```
┌──────────────┐     OPC UA     ┌──────────────┐         HTTP         ┌──────────────┐
│  Factory IO  │ ◄────────────► │  TIA Portal  │ ◄──────────────────► │    Bridge    │
│  (3D Sim)    │                │     (PLC)    │                      │  (OPC UA +   │
└──────────────┘                └──────────────┘                      │   HTTP)      │
                                                                      └──────┬───────┘
                                                                             │
                                                                        HTTP │
                                                                             ▼
┌──────────────┐    SignalR     ┌──────────────────────────────────────────────────┐
│    React     │ ◄────────────► │              Backend Server                      │
│   Frontend   │     HTTP       │  ┌─────────┐ ┌─────────┐ ┌──────────────────┐    │
└──────────────┘                │  │   API   │ │ SignalR │ │  Memory Cache    │    │
                                │  │ (REST)  │ │  Hub    │ │  (Repository)    │    │
                                │  └─────────┘ └─────────┘ └──────────────────┘    │
                                └──────────────────────────────────────────────────┘
```

## 🏗️ Architecture

The project follows **Clean Architecture** with four distinct layers:

```
┌─────────────────────────────────────────────────────────────────┐
│                        PRESENTATION                              │
│  Controllers │ Hubs │ Middleware │ Services │ Program.cs        │
├─────────────────────────────────────────────────────────────────┤
│                        APPLICATION                               │
│  Services │ Interfaces │ Validators │ Options                    │
├─────────────────────────────────────────────────────────────────┤
│                        INFRASTRUCTURE                            │
│  Repositories │ Providers │ External Services                    │
├─────────────────────────────────────────────────────────────────┤
│                          DOMAIN                                  │
│  Entities │ DTOs │ Exceptions │ Business Rules                   │
└─────────────────────────────────────────────────────────────────┘
```

---

## Responsibilities

- Maintain the authoritative state of OPC UA nodes
- Broadcast updates via SignalR
- Expose REST endpoints for management and diagnostics
- Provide in-memory caching for high-performance reads

---

## 🛠️ Technologies

| Technology | Purpose |
|-----------|---------|
| .NET 10 | Runtime & SDK |
| C# 13 | Modern language features |
| ASP.NET Core | Web API |
| SignalR | Real-time messaging |
| FluentValidation | Input validation |
| MemoryCache | High-performance caching |

---

## API Overview

### Base URL

```
http://localhost:5000/api/opcua-nodes
```

### Endpoints

| Method | Endpoint | Description |
|-------|---------|-------------|
| GET | `/` | List all nodes |
| GET | `/{name}` | Get node by name |
| POST | `/` | Create node |
| PUT | `/{name}` | Update node |
| DELETE | `/{name}` | Delete node |

---

## SignalR Hub

### Hub URL

```
/hub/opcua-nodes
```

### Responsibilities

- Push real-time node changes to clients
- Send initial state on subscription
- Synchronize simulation front data

---

## 🚀 Getting Started

### Prerequisites

- .NET 10 SDK

1. **Clone the repository**
   ```bash
   git clone https://github.com/igor-fuchs/Embedded-Integration-Backend.git
   cd Embedded-Integration-Backend/Server
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the server**
   ```bash
   dotnet run --project src/Presentation
   ```

### Configuration

The application is configured via `appsettings.json`:

```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://0.0.0.0:5000"
      }
    }
  },
  "SimulationFront": {
    "GroupName": "SimulationFront",
    "Nodes": {
      "ActuatorAinAdvance": "ns=3;s=\"...\".\"CommandAdvance\"",
      "ConveyorLeftRunning": "ns=3;s=\"...\".\"RUNNING\"",
      "RobotLeftToHome": "ns=3;s=\"...\".\"GoingToHome\"",
      ...
    }
  }
}
```

---

## Role in the Ecosystem

The backend is the **core mediator**:

- Receives updates from the Bridge
- Validates and caches industrial states
- Broadcasts consistent data to all front-end clients

It ensures **decoupling** between UI and industrial protocols.

---

## 📄 License

This project is for educational and demonstration purposes.

## 👤 Author

**Igor Fuchs**

- GitHub: [@igor-fuchs](https://github.com/igor-fuchs)
- LinkedIn: [Igor Fuchs Pereira](www.linkedin.com/in/igor-fuchs-pereira)

---

<div align="center">

**Part of the Embedded Integration project ecosystem**

[Frontend Repository](https://github.com/igor-fuchs/Embedded-Integration-Frontend) • [Bridge Repository](https://github.com/igor-fuchs/Embedded-Integration-Bridge)

</div>

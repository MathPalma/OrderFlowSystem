# 🚀 OrderFlowSystem

## 📖 Project Description

**OrderFlowSystem** is a system for managing order flow, divided into two main parts: a **REST API** for creating and consulting orders and an asynchronous **Worker Service** for processing. The project follows a **Microservices** architecture, using **RabbitMQ** for communication between the API and the Worker, ensuring scalability and resilience.

The architecture was designed based on **Domain-Driven Design (DDD)** principles, with a clear separation of **Domain**, **Application**, and **Infrastructure** layers.
## 🛠️ Technologies Used

* **ASP.NET 6**
* **C#**
* **Entity Framework Core**
* **SQL Server** (or another EF Core-compatible database)
* **RabbitMQ**
* **Docker** (recommended for running RabbitMQ locally)

## ⚙️ How to Run the Project

Follow the steps below to clone, configure, and run the solution.

### Prerequisites

* **.NET 6 SDK** installed.
* **SQL Server** (or Docker to run a database container).
* **RabbitMQ** (recommended to use Docker).

### Step 1: Clone the Repository

```bash
git clone [https://github.com/your-username/OrderFlowSystem.git](https://github.com/your-username/OrderFlowSystem.git)
cd OrderFlowSystem
```
### Step 2: Configure the Database

1.  Open the **`API/appsettings.json`** file and configure your database connection string.

2.  Apply the Entity Framework migrations to create the database and tables:

    ```bash
    dotnet ef database update --project API/API.csproj
    ```

### Step 3: Configure RabbitMQ

1.  Ensure that RabbitMQ is running. If you're using Docker, you can run it with the following command:

    ```bash
    docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
    ```

2.  Access the RabbitMQ management panel at `http://localhost:15672` (default user/password: `guest`/`guest`).

3.  Verify the connection configuration in the **`Worker/appsettings.json`** file (the default `localhost` should work).

### Step 4: Run the Projects

Open two terminal instances and run each project separately:

#### 1. Run the API

```bash
cd API
dotnet run
```
The API will be available at `http://localhost:5000` (or the port configured in `launchSettings.json`).

#### 2. Run the Worker Service

```bash
cd Worker
dotnet run
```
The Worker Service will start listening to the RabbitMQ queue

 E-Commerce Microservices



The solution is built around several architectural pillars essential for modern software engineering.

 1. Advanced Data Access
We use a diverse data access strategy tailored to specific service needs:
   Entity Framework Core: Powering the Catalog Service for sophisticated object-relational mapping.
   Dapper: Integrated into the Legacy Inventory service for high-speed, direct SQL interaction.
   Repository Pattern: Standardizes data operations across all services, providing a clean abstraction.
   CQRS with MediatR: Segregates read and write paths in the catalog service for efficient processing.

2. Implementation of Design Patterns
The codebase serves as a clear example of several essential design patterns:
   Mediator Pattern: Decouples command issuance from execution, resulting in a cleaner controller layer.
   Singleton Pattern: Efficiently manages shared configurations and state within the inventory infrastructure.
   Dependency Injection: Used throughout the solution to ensure loose coupling and testability.
   Proxy / Gateway Client: Implemented to act as a centralized gateway to the microservices.
   Typed Client: Utilized for specialized service-to-service communication with built-in resilience.
   Reverse Proxy (YARP): Leveraged in the API Gateway to provide robust routing.

3. Microservices Architecture
The system is divided into specialized, independently deployable services:
   API Gateway: Handles centralized orchestration and request routing.
   Catalog Service: Manages the product life cycle and search operations.
   Legacy Inventory: Simulates back-end integration with existing systems.
   Order Service: Coordinates cross-service transactions to ensure inventory consistency.
   All service-to-service interactions are handled via resilient HTTP clients.

4. Docker and Containerization
The project uses containerization for a consistent deployment model:
   Multi-Stage Builds: Optimized Dockerfiles that separate build-time dependencies from the final runtime images.
   Docker Compose: Manages the startup and orchestration of five microservices and a shared SQL Server instance.
   Network Isolation: Services communicate over a dedicated internal network, keeping the internal architecture secure.


 Professional Engineering Standards

The project follows rigorous coding standards to ensure code quality:
   Thorough Documentation: Every class, method, and property is fully documented with XML summaries.
   Standardized Naming: All private fields follow a strict ALLCAPS convention (e.g., GATEWAYSERVICE, MEDIATOR) for clarity.
   Clean Codebase: All default template code and boilerplate have been removed to keep the project focused and professional.

Getting Started

1.  Run with Docker: Open a terminal in the root directory and run:
    ```bash
    docker-compose up --build
    ```
2.  Access the App: Navigate to [http://localhost:8080](http://localhost:8080).
3.  Admin Login: Use `admin` / `password` to access protected features.

Docker Maintenance

If you need to reset the environment or perform a clean rebuild, use the following commands:

   Stop and Remove Containers: 
   To shut down the entire system and remove the containers, run:
   ```bash
   docker-compose down
   ```
   Clean Rebuild: 
   If you've made significant changes or want to ensure a fresh start without cached layers, run:
   ```bash
   docker-compose up --build --no-cache
   ```
   Complete Reset: 
   To remove all containers and their associated images, run:
    ```bash
    docker-compose down --rmi all
    ```



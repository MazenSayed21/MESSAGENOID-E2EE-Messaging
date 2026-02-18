# MESSAGENOID

A secure, anonymous messaging platform built with a focus on privacy and layered architectural standards.

## 📝 Project Concept
**MESSAGENOID** is a web-based messaging application that prioritizes user anonymity and data privacy. It allows users to send messages to any other registered user without revealing their identity. 

## 🔐 Security: End-to-End Encryption (E2EE)
The application implements a **Zero-Knowledge** security model to ensure total privacy:
* **Client-Side Encryption:** All messages are encrypted locally on the sender's device before being transmitted.
* **Server-Side Privacy:** Data stored in the database is completely encrypted. Even with direct access to the database, neither an external intruder nor the application developer can read the message contents.



## 🏗️ Technical Architecture
The project is built using a **Layered Architecture** to ensure clean code, scalability, and a strict separation of concerns:

* **Controllers:** Act as the entry point for the application. They receive incoming HTTP requests, orchestrate the flow by fetching relevant data from the Services, and return the appropriate responses to the Views.
* **Services:** Handle the core application logic and business rules, acting as the bridge between the controllers and the data access layer.
* **Repositories:** Dedicated to managing database interactions and data persistence, ensuring the application is decoupled from the specific database implementation.
* **Views / UI:** The presentation layer that renders the interface and handles user interaction.



## 🔄 Data Flow Process
1. **Request:** The user interacts with the **View**, sending a request to the **Controller**.
2. **Orchestration:** The **Controller** receives the request and calls the appropriate **Service**.
3. **Logic & Data:** The **Service** processes the business logic and uses the **Repository** to interact with the database.
4. **Response:** The **Controller** receives the processed data and returns the final response back to the **View**.

## 🚀 Key Features
* **True Anonymity:** Send messages to any user without identity disclosure.
* **E2EE Security:** Industry-standard encryption ensuring no unauthorized access to message content.
* **Modular Design:** Structured with professional architectural patterns for easy maintenance.

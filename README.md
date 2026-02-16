# Transaction System - Console Application

Тransaction system written in **C# (.NET 8)** for softuni web exam task.  

---

## 📌 Features

### Core Operations
- Create Account  
- Deposit  
- Withdraw  
- Transfer between accounts  (TODO)

### Architecture Highlights
- **Separation of concerns** across:
  - `TransactionSystem.Core`
  - `TransactionSystem.Data`
  - `TransactionSystem.Models`
  - `TransactionSystem.Web`

- **Unit of Work Pattern**  
- **Repository Pattern**  
- **DTO-based communication** 

### Database Provider
- **SQL Server

### NUnit Unit Tests
- Uses the **In-Memory Data Layer** for isolated testing  
- Covers:
  - Account creation
  - Deposit / Withdraw
  - Transfers
  - Error cases

---

## Database Structure (MSSQL)

![Database Diagram](assets/db_schhema.png)

### **Account**
| Column        | Type    |
|---------------|---------|
| Id            | int PK  |
| AccountNumber | int     | (Unique)
| FullName      | string  |
| Balance       | decimal |

### **Deposit**
| Column    | Type    |
|-----------|---------|
| Id        | int PK  |
| AccountId | FK → Account.Id |
| Money     | decimal |
| Date      | text    | (Datetime)

### **Withdraw**
| Column    | Type    |
|-----------|---------|
| Id        | int PK |
| AccountId | FK → Account.Id |
| Money     | decimal |
| Date      | text    | (Datetime)

### **Transfer**
| Column        | Type    |
|---------------|---------|
| Id            | int PK |
| FromAccountId | FK → Account.Id |
| ToAccountId   | FK → Account.Id |
| Money         | decimal |
| Date          | text    | (Datetime)

---


## StartUp 
TransactionSystem.Web -> Program.cs

Enjoy exploring the project!